// <copyright file="MainForm.cs" company="PublicDomain.com">
//     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
//     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// </copyright>

namespace DateTimeDirectory
{
    // Directives
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using Microsoft.Win32;
    using PublicDomain;

    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The name of the icon file.
        /// </summary>
        private string iconFileName = "DateTimeDirectory-menu-icon.ico";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DateTimeDirectory.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // The InitializeComponent() call is required foCr Windows Forms designer support.
            this.InitializeComponent();

            // Set associated icon from exe file
            this.AssociatedIcon = Icon.ExtractAssociatedIcon(typeof(MainForm).GetTypeInfo().Assembly.Location);

            // Set public domain daily tool strip menu item image
            this.dailyReleasesPublicDomainDailycomToolStripMenuItem.Image = this.AssociatedIcon.ToBitmap();

            // Update the program by DateTimeDirectory key
            this.UpdateByDateTimeDirectoryRegistryKey();
        }

        /* Internal access modifier inherited from "Consolidate Directory" */

        /// <summary>
        /// Gets or sets the associated icon.
        /// </summary>
        /// <value>The associated icon.</value>
        internal Icon AssociatedIcon { get; set; }

        /// <summary>
        /// Handles the daily releases public domain dailycom tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        internal void OnDailyReleasesPublicDomainDailycomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open current website
            Process.Start("https://publicdomaindaily.com");
        }

        /// <summary>
        /// Handles the original thread donation codercom tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        internal void OnOriginalThreadDonationCodercomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open original thread @ DonationCoder
            Process.Start("https://www.donationcoder.com/forum/index.php?topic=46534.0");
        }

        /// <summary>
        /// Handles the source code githubcom tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        internal void OnSourceCodeGithubcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open GitHub
            Process.Start("https://github.com/publicdomain/datetime-directory");
        }

        /// <summary>
        /// Handles the about tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        internal void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Set license text
            var licenseText = $"CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication{Environment.NewLine}" +
                $"https://creativecommons.org/publicdomain/zero/1.0/legalcode{Environment.NewLine}{Environment.NewLine}" +
                $"Libraries and icons have separate licenses.{Environment.NewLine}{Environment.NewLine}" +
                $"Folder new icon by OpenIcons - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/vectors/folder-new-add-icon-index-app-97593/{Environment.NewLine}{Environment.NewLine}" +
                $"Patreon icon used according to published brand guidelines{Environment.NewLine}" +
                $"https://www.patreon.com/brand{Environment.NewLine}{Environment.NewLine}" +
                $"GitHub mark icon used according to published logos and usage guidelines{Environment.NewLine}" +
                $"https://github.com/logos{Environment.NewLine}{Environment.NewLine}" +
                $"DonationCoder icon used with permission{Environment.NewLine}" +
                $"https://www.donationcoder.com/forum/index.php?topic=48718{Environment.NewLine}{Environment.NewLine}" +
                $"PublicDomain icon is based on the following source images:{Environment.NewLine}{Environment.NewLine}" +
                $"Bitcoin by GDJ - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/vectors/bitcoin-digital-currency-4130319/{Environment.NewLine}{Environment.NewLine}" +
                $"Letter P by ArtsyBee - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/illustrations/p-glamour-gold-lights-2790632/{Environment.NewLine}{Environment.NewLine}" +
                $"Letter D by ArtsyBee - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/illustrations/d-glamour-gold-lights-2790573/{Environment.NewLine}{Environment.NewLine}";

            // Set title
            string programTitle = typeof(MainForm).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;

            // Set version for generating semantic version 
            Version version = typeof(MainForm).GetTypeInfo().Assembly.GetName().Version;

            // Set about form
            var aboutForm = new AboutForm(
                $"About {programTitle}",
                $"{programTitle} {version.Major}.{version.Minor}.{version.Build}",
                $"Made for: nkormanik, Curt, nogojoe{Environment.NewLine}DonationCoder.com{Environment.NewLine}Day #273, Week #40 @ September 29, 2020",
                licenseText,
                this.Icon.ToBitmap());

            // Set about form icon
            aboutForm.Icon = this.AssociatedIcon;

            // Show about form
            aboutForm.ShowDialog();
        }

        /// <summary>c
        /// Handles the add button click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnAddButtonClick(object sender, EventArgs e)
        {
            try
            {
                /* Add DateTimeDirectory command to registry */

                // Delare registry key
                RegistryKey registryKey;

                // Left
                registryKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\directory\shell\DateTimeDirectory");
                registryKey.SetValue("icon", $"{Path.Combine(Application.StartupPath, this.iconFileName)}");
                registryKey.SetValue("position", "Top");
                registryKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\directory\shell\DateTimeDirectory\command");
                registryKey.SetValue(string.Empty, $"{Path.Combine(Application.StartupPath, Application.ExecutablePath)} \"%1\"");

                // Right
                registryKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\directory\Background\shell\DateTimeDirectory");
                registryKey.SetValue("icon", $"{Path.Combine(Application.StartupPath, this.iconFileName)}");
                registryKey.SetValue("position", "Top");
                registryKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\directory\Background\shell\DateTimeDirectory\command");
                registryKey.SetValue(string.Empty, $"{Path.Combine(Application.StartupPath, Application.ExecutablePath)} \"%1\"");

                // Close registry key
                registryKey.Close();

                // Create icon if it does not exist
                if (!File.Exists(this.iconFileName))
                {
                    // Use file stream
                    using (FileStream fileStream = File.Create(this.iconFileName))
                    {
                        // Save main form icon to file
                        this.Icon.Save(fileStream);
                    }
                }

                // Update the program by DateTimeDirectory key
                this.UpdateByDateTimeDirectoryRegistryKey();

                // Notify user
                MessageBox.Show($"DateTimeDirectory context menu added!{Environment.NewLine}{Environment.NewLine}Right-click folder in Windows Explorer to use.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Notify user
                MessageBox.Show($"Error when adding DateTimeDirectory context menu to registry.{Environment.NewLine}{Environment.NewLine}Message:{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the remove button click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnRemoveButtonClick(object sender, EventArgs e)
        {
            try
            {
                // Remove DateTimeDirectory command from registry
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\directory\shell\DateTimeDirectory");
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\directory\Background\shell\DateTimeDirectory");

                // Remove icon
                File.Delete(this.iconFileName);

                // Update the program by DateTimeDirectory key
                this.UpdateByDateTimeDirectoryRegistryKey();

                // Notify user
                MessageBox.Show("DateTimeDirectory context menu removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Notify user
                MessageBox.Show($"Error when removing DateTimeDirectory command from registry.{Environment.NewLine}{Environment.NewLine}Message:{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates the program by DateTimeDirectory registry key.
        /// </summary>
        private void UpdateByDateTimeDirectoryRegistryKey()
        {
            // Try to set DateTimeDirectory key
            using (var dateTimeDirectoryKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\directory\shell\DateTimeDirectory"))
            {
                // Check for no returned registry key
                if (dateTimeDirectoryKey == null)
                {
                    // Disable remove button
                    this.removeButton.Enabled = false;

                    // Enable add button
                    this.addButton.Enabled = true;

                    // Update status text
                    this.activityToolStripStatusLabel.Text = "Inactive";
                }
                else
                {
                    // Disable add button
                    this.addButton.Enabled = false;

                    // Enable remove button
                    this.removeButton.Enabled = true;

                    // Update status text
                    this.activityToolStripStatusLabel.Text = "Active";
                }
            }
        }

        /// <summary>
        /// Handles the exit tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Close application
            this.Close();
        }
    }
}
