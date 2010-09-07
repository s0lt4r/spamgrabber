﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Outlook;
using System.IO;
using System.Windows.Forms;
using SpamGrabberControl;

namespace SpamGrabber
{
    public partial class SpamGrabber_Ribbon
    {
        #region Button Event Handlers

        private void btnReportDefaultSpam_Click(object sender, RibbonControlEventArgs e)
        {
            if (string.IsNullOrEmpty(SpamGrabberCommon.GlobalSettings.DefaultSpamProfileId))
            {
                MessageBox.Show("You have not yet set a default spam profile. Please open the SpamGrabber settings dialog and set a default spam profile");
                return;
            }
            SendReports(SpamGrabberCommon.GlobalSettings.DefaultSpamProfileId);
        }

        private void btnReportDefaultHam_Click(object sender, RibbonControlEventArgs e)
        {
            if (string.IsNullOrEmpty(SpamGrabberCommon.GlobalSettings.DefaultHamProfileId))
            {
                MessageBox.Show("You have not yet set a default ham profile. Please open the SpamGrabber settings dialog and set a default ham profile");
                return;
            }
            SendReports(SpamGrabberCommon.GlobalSettings.DefaultHamProfileId);
        }

        private void btnCopyToClipboard_Click(object sender, RibbonControlEventArgs e)
        {
            Explorer exp = Globals.ThisAddIn.Application.ActiveExplorer();
            if (exp.Selection.Count > 0)
            {
                Clipboard.SetText(GetMessageSource((MailItem)exp.Selection[1], false));
            }
        }

        private void btnSettings_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                frmOptions myOptions = new frmOptions();
                myOptions.ShowDialog();
                if (myOptions.DialogResult == DialogResult.OK)
                {
                    // Refresh the drop down items
                    this.LoadDropDown();
                    // Refresh the command bar
                    this.ShowHideButtons();
                }
            }
            catch (System.Exception ex) // TODO we should not catch all exceptions
            {
                MessageBox.Show("caught: \r\n" + ex.ToString());
            }
        }

        private void btnReportCustom_Click(object sender, RibbonControlEventArgs e)
        {
            if (this.ddlReportTo.SelectedItem != null)
            {
                this.SendReports(this.ddlReportTo.SelectedItem.Tag.ToString());
            }
        }

        private void btnSafeView_Click(object sender, RibbonControlEventArgs e)
        {
            Explorer exp = Globals.ThisAddIn.Application.ActiveExplorer();
            if (exp.Selection.Count > 0)
            {
                frmPreview objPreview = new frmPreview();
                objPreview.ClearItems();
                foreach (object objItem in exp.Selection)
                {
                    if (objItem is MailItem || objItem is PostItem)
                        objPreview.Items.Add(objItem);
                }
                objPreview.ShowDialog();
            }
        }

        #endregion

        #region Common Functions

        private void SendReports(string profileID)
        {
            SpamGrabberCommon.Profile profile = new SpamGrabberCommon.Profile(profileID);

            if (profile.AskVerify)
            {
                if (MessageBox.Show("Are you sure you want to report the selected item(s)?", "Report messages", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            Explorer exp = Globals.ThisAddIn.Application.ActiveExplorer();

            // Create a collection to hold references to the attachments
            List<string> attachmentFiles = new List<string>();

            // Make sure at least one item is sent
            bool bItemsSelected = false;

            // First make sure the selected emails have been downloaded
            bool bNeedsSendReceive = false;
            for (int i = 1; i <= exp.Selection.Count; i++)
            {
                if (exp.Selection[i] is MailItem)
                {
                    MailItem mail = (MailItem)exp.Selection[i];
                    bItemsSelected = true;
                    // If the item has not been downloaded, mark for download
                    if (mail.DownloadState == OlDownloadState.olHeaderOnly)
                    {
                        bNeedsSendReceive = true;
                        mail.MarkForDownload = OlRemoteStatus.olMarkedForDownload;
                        mail.Save();
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mail);
                }
            }
            if (bNeedsSendReceive)
            {
                // Download the marked emails
                // TODO: Trying to carry on at this point returns blank email bodies. Try and find a way of downloading them properly.
                Globals.ThisAddIn.Application.Session.SendAndReceive(false);
                MessageBox.Show("One of more emails were not downloaded from the server. Please ensure they are now downloaded and click report again",
                    "SpamGrabber", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (bItemsSelected)
            {
                // Now get references to all the items
                for (int i = 1; i <= exp.Selection.Count; i++)
                {
                    if (exp.Selection[i] is MailItem)
                    {
                        MailItem mail = (MailItem)exp.Selection[i];
                        if (profile.UseRFC)
                        {
                            // Direct attaching seems to be buggy. Save the mailitem first
                            string fileName = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".msg");
                            mail.SaveAs(fileName);
                            attachmentFiles.Add(fileName);
                        }
                        else
                        {
                            // Create temp text file
                            string fileName = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".txt");
                            TextWriter tw = new StreamWriter(fileName);
                            tw.Write(GetMessageSource(mail, profile.CleanHeaders));
                            tw.Close();
                            attachmentFiles.Add(fileName);
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(mail);
                    }
                }
            
                // Are we using a single email or one per report?
                if (profile.SendMultiple)
                {
                    // Create the report email
                    MailItem reportEmail = CreateReportEmail(profile);

                    // Attach the files
                    foreach (string attachment in attachmentFiles)
                    {
                        reportEmail.Attachments.Add(attachment);
                    }

                    // Send the report
                    reportEmail.Send();

                    // Do we need to keep a copy?
                    if (!profile.KeepCopy)
                    {
                        reportEmail.Delete();
                    }
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(reportEmail);
                }
                else
                {
                    // Send one email per report
                    foreach (string attachment in attachmentFiles)
                    {
                        MailItem reportEmail = CreateReportEmail(profile);
                        reportEmail.Attachments.Add(attachment);
                        reportEmail.Send();
                        // Do we need to keep a copy?
                        if (!profile.KeepCopy)
                        {
                            reportEmail.Delete();
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(reportEmail);
                    }
                }

                // Sort out actions on the source emails
                for (int i = 1; i <= exp.Selection.Count; i++)
                {
                    if (exp.Selection[i] is MailItem)
                    {
                        MailItem mail = (MailItem)exp.Selection[i];
                        if (profile.MarkAsReadAfterReport)
                        {
                            mail.UnRead = false;
                        }
                        if (profile.DeleteAfterReport)
                        {
                            mail.Delete();
                        }
                        else if (profile.MoveToFolderAfterReport)
                        {
                            mail.Move(Globals.ThisAddIn.Application.GetNamespace("MAPI").GetFolderFromID(
                                profile.MoveFolderName, profile.MoveFolderStoreId));
                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(mail);
                    }
                }
            }
        }

        private void SpamGrabber_Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            // Load the drop down items
            this.LoadDropDown();
            // Show / hide buttons based on settings
            this.ShowHideButtons();
        }

        private void LoadDropDown()
        {
            this.ddlReportTo.Items.Clear();
            foreach (SpamGrabberCommon.Profile profile in SpamGrabberCommon.UserProfiles.ProfileList)
            {
                RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                item.Label = profile.Name;
                item.Tag = profile.Id;
                this.ddlReportTo.Items.Add(item);
            }
        }

        private string GetMessageSource(MailItem message, bool cleanHeaders)
        {
            string headers = message.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x007D001E");
            return string.Format("{1}{0}{2}", Environment.NewLine,
                cleanHeaders ? SpamGrabberCommon.SGGlobals.RepairHeaders(headers, message.BodyFormat.Equals(OlBodyFormat.olFormatHTML)) : headers,
                message.BodyFormat == OlBodyFormat.olFormatHTML ? message.HTMLBody : message.Body);
        }

        private void ShowHideButtons()
        {
            this.btnReportDefaultHam.Visible = SpamGrabberCommon.GlobalSettings.ShowHamButton;
            this.btnCopyToClipboard.Visible = SpamGrabberCommon.GlobalSettings.ShowCopyButton;
            this.btnSafeView.Visible = SpamGrabberCommon.GlobalSettings.ShowPreviewButton;
            this.gpSettings.Visible = SpamGrabberCommon.GlobalSettings.ShowSettingsButton;
            this.boxReportTo.Visible = SpamGrabberCommon.GlobalSettings.ShowSelectButton;
        }

        private MailItem CreateReportEmail(SpamGrabberCommon.Profile profile)
        {
            // Create the report email
            MailItem reportEmail = (MailItem)Globals.ThisAddIn.Application.CreateItem(OlItemType.olMailItem);
            reportEmail.Subject = profile.ReportSubject;
            foreach (string toAddress in profile.ToAddresses)
            {
                reportEmail.To += toAddress + ";";
            }
            foreach (string bccAddress in profile.BccAddresses)
            {
                reportEmail.BCC += bccAddress + ";";
            }
            reportEmail.BodyFormat = OlBodyFormat.olFormatPlain;
            reportEmail.Body = profile.MessageBody;
            return reportEmail;
        }

        #endregion
    }
}