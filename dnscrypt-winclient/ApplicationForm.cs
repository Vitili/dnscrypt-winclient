using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
namespace dnscrypt_winclient
{
	public partial class ApplicationForm : Form
	{
		public const int SW_SHOWMINIMIZED = 2;
		public const int SW_HIDE = 0;

        public Dictionary<string, List<String>> dnsServerslist = new Dictionary<string, List<String>>();
        public Dictionary<string, List<String>> dnsServersv6list = new Dictionary<string, List<String>>();

        private bool shouldIgnoreCheckEvent = false;

		private ProcessStartInfo CryptProc = null;
		private Process CryptHandle = null;
		private bool CryptProcRunning = false;


		public ApplicationForm()
		{
			InitializeComponent();
            setdnsServers();
			GetNICs(false,false);
			this.portBox.SelectedIndex = 1;	//Port 443
            this.serverBox.SelectedIndex = 0; // CloudNS.com.au
            this.service_button.Text = "Start";
            status_update(0);
		}



        #region Start Button
        /// <summary>
		/// Button to start and stop the DNSCrypt Proxy application
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void service_button_Click(object sender, EventArgs e)
		{
			// Start the DNSCrypt process
			if (!this.CryptProcRunning)
			{
				// Make sure the file exists before trying to launch it
				if (!File.Exists(Directory.GetCurrentDirectory() + "\\dnscrypt-proxy.exe"))
				{
                    MessageBox.Show("dnscrypt-proxy.exe was not found. It should be placed in the same directory as this program. If you do not have this file, you can download it from http://download.dnscrypt.org/dnscrypt-proxy/", "File not found");
					return;
                }

                //-----------Disable controls---------
                this.gatewayCheckbox.Enabled = false;
                this.ipv4Radio.Enabled = false;
                this.ipv6Radio.Enabled = false;
                this.serverBox.Enabled = false;
                this.protoTCP.Enabled = false;
                this.protoUDP.Enabled = false;
                this.portBox.Enabled = false;
                this.DNSlistbox.Enabled = false;
                this.parentalControlsRadio.Enabled = false;
                this.hideAdaptersCheckbox.Enabled = false; 
                //------------Process-----------------------
                this.CryptProc = new ProcessStartInfo();
                this.CryptProc.FileName = "dnscrypt-proxy.exe";
                this.CryptProc.RedirectStandardOutput = true;
                this.CryptProc.RedirectStandardError = true;
                this.CryptProc.RedirectStandardInput = true;
                this.CryptProc.UseShellExecute = false;
                this.CryptProc.CreateNoWindow = true;

                //-------------End of line-------------------


                #region Ip list & Arguments
                //---------------------------------------------- IP Settings----------------------------------------             

				if (this.protoTCP.Checked)
				{
					this.CryptProc.Arguments = "-T";
				}
                
                switch (this.serverBox.SelectedIndex)
                {
                    case 0:             //----------------------- "1.OpenDNS               [USA]"
                        if (this.ipv6Radio.Checked)
                        {
                            this.CryptProc.Arguments += " --resolver-address=[2620:0:ccd::2]";
                        }
                        else if (this.parentalControlsRadio.Checked)
                        {
                            this.CryptProc.Arguments += " --resolver-address=208.67.220.123";
                        }
                        else
                        {
                            this.CryptProc.Arguments += " --resolver-address=208.67.220.220";
                        }
                        break;
                    case 1:             //----------------------- "2.CloudNS.com.au    [Canberra, Australia]"
                        if (this.ipv6Radio.Checked)
                        {
                            this.CryptProc.Arguments += " -r 2405:5000:2:1e5:250:56ff:fe9a:35b --provider-name=2.dnscrypt-cert.cloudns.com.au --provider-key=1971:7C1A:C550:6C09:F09B:ACB1:1AF7:C349:6425:2676:247F:B738:1C5A:243A:C1CC:89F4";
                        }
                        else
                        {
                            this.CryptProc.Arguments += " -r 113.20.6.2:443 --provider-name=2.dnscrypt-cert.cloudns.com.au --provider-key=1971:7C1A:C550:6C09:F09B:ACB1:1AF7:C349:6425:2676:247F:B738:1C5A:243A:C1CC:89F4";
                        }
                        break;

                    case 2:             //----------------------- "3.CloudNS.com.au    [Sydney, Australia]"
                        if (this.ipv6Radio.Checked)
                        {
                            this.CryptProc.Arguments += " -r 2405:5000:2:1e5:250:56ff:fe9a:35b --provider-name=2.dnscrypt-cert.cloudns.com.au --provider-key=1971:7C1A:C550:6C09:F09B:ACB1:1AF7:C349:6425:2676:247F:B738:1C5A:243A:C1CC:89F4";
                        }
                        else
                        {
                            this.CryptProc.Arguments += " -r 113.20.8.17:443 --provider-name=2.dnscrypt-cert-2.cloudns.com.au --provider-key=67A4:323E:581F:79B9:BC54:825F:54FE:1025:8B4F:37EB:0D07:0BCE:4010:6195:D94F:E330";
                        }
                        break;

                    case 3:             //----------------------- "4.OpenNIC                [Japan]"
                            this.CryptProc.Arguments += " -r 106.186.17.181:2053 --provider-name=2.dnscrypt-cert.ns2.jp.dns.opennic.glue --provider-key=8768:C3DB:F70A:FBC6:3B64:8630:8167:2FD4:EE6F:E175:ECFD:46C9:22FC:7674:A1AC:2E2A";
                        break;

                    case 4:             //----------------------- "5.DNSCrypt.eu          [Holland]"
                            this.CryptProc.Arguments += " -r 176.56.237.171:443 --provider-name=2.dnscrypt-cert.dnscrypt.eu --provider-key=67C0:0F2C:21C5:5481:45DD:7CB4:6A27:1AF2:EB96:9931:40A3:09B6:2B8D:1653:1185:9C66";
                        break;

                }

              
				this.CryptProc.Arguments += ":" + this.portBox.SelectedItem.ToString()+ " --loglevel=10";

				if (this.gatewayCheckbox.Checked)
				{
					this.CryptProc.Arguments += " --local-address=0.0.0.0";
				}

                //--------------------------------------End of IP settings---------------------------------------
#endregion

                //----------------Create Process --------------------------
				this.CryptHandle = Process.Start(this.CryptProc);
                this.CryptHandle.StartInfo = this.CryptProc;
                this.CryptHandle.OutputDataReceived +=cmd_DataReceived;
                this.CryptHandle.EnableRaisingEvents = true;
                this.CryptHandle.BeginOutputReadLine();
                this.CryptHandle.BeginErrorReadLine();

                //------------------------------------------


                this.textBox1.Text = "";
                this.service_button.Text = "Stop";
                this.status_update(0);
				this.CryptProcRunning = true;
                tmr.Enabled = true;
			}
			else
			{
				// Make sure the proxy wasn't terminated by another application/user
				if (!this.CryptHandle.HasExited)
				{
					this.CryptHandle.Kill();
				}
  
                this.CryptHandle.Kill();
				this.CryptProc = null;
                this.service_button.Text = "Start";
                this.status_update(0);
				this.CryptProcRunning = false;
                this.gatewayCheckbox.Enabled = true;
                this.parentalControlsRadio.Enabled = true;
                this.ipv4Radio.Enabled = true;
                this.ipv6Radio.Enabled = true;
                this.serverBox.Enabled = true;
                this.protoTCP.Enabled = true;
                this.protoUDP.Enabled = true;
                this.portBox.Enabled = true;
                this.DNSlistbox.Enabled = true;
                this.hideAdaptersCheckbox.Enabled = true;
                tmr.Enabled = false;
   			}
            
		}

        #endregion

        #region DNS Events

        private void setdnsServers()
        {
            int i = 0;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();

                List<string> dnsServers = new List<string>();
                List<string> dnsServersv6 = new List<string>();

                //Determine if the DNS addresses are obtained automatically or specified
                //We don't want to add them when the program is closed if they weren't actually set
                //Unfortunately, neither WMI nor the NetworkInterface class provide information about this setting
                dnsServers = NetworkManager.getDNS(adapter.Id);
                dnsServersv6 = NetworkManager.getDNSv6(adapter.Id);
                dnsServerslist.Add(adapter.Id, dnsServers);
                dnsServersv6list.Add(adapter.Id, dnsServersv6);
                i++;
            }

        }

        /// <summary>
        /// Fills the ListBox with information about each NIC
        /// </summary>
        private void GetNICs(Boolean showHidden, Boolean refresh)
        {
         
            this.ipv6Radio.Enabled = false;

            int i = 0;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();

                List<string> dnsServers = new List<string>();
                List<string> dnsServersv6 = new List<string>();

                //Determine if the DNS addresses are obtained automatically or specified
                //We don't want to add them when the program is closed if they weren't actually set
                //Unfortunately, neither WMI nor the NetworkInterface class provide information about this setting
                dnsServers = NetworkManager.getDNS(adapter.Id);
                dnsServersv6 = NetworkManager.getDNSv6(adapter.Id);

                NetworkListItem item = new NetworkListItem(adapter.Id, adapter.Name, adapter.Description, dnsServers, dnsServersv6, adapter.Supports(NetworkInterfaceComponent.IPv4), adapter.Supports(NetworkInterfaceComponent.IPv6));
                if (!item.hidden || showHidden)
                {
                    if (refresh == false)
                    {
                        if (dnsServers.Count != 0)
                        {
                            if (dnsServers[0] == "127.0.0.1")
                            {
                                DNSlistbox.Items.Add(item, true);
                            }
                            else
                            {
                                DNSlistbox.Items.Add(item, false);
                            }
                        }
                        else
                        {
                            DNSlistbox.Items.Add(item, false);
                        }


                        //See if the device supports IPv6 and enable the option if so
                        if (item.IPv6)
                        {
                            this.ipv6Radio.Enabled = true;
                        }
                    }
                    else
                    {
                        if (DNSlistbox.Items.Count != 0 & DNSlistbox.Items.Count > 1)
                        {
 
                            DNSlistbox.Items[i] = item;
                            //See if the device supports IPv6 and enable the option if so
                            if (item.IPv6)
                            {
                                this.ipv6Radio.Enabled = true;
                            }
                        }
                    }
                }



                i++;

                /*
                IPAddressCollection dnsServers2 = adapterProperties.DnsAddresses;
                if (dnsServers2.Count > 0)
                {
                    //NetworkListItem item = new NetworkListItem(adapter.Description, dnsServers2);
                    //DNSlistbox.Items.Add(item);

                    Console.WriteLine(adapter.Description + ": " + adapter.Id);
                    foreach (IPAddress dns in dnsServers2)
                    {
                        Console.WriteLine("  DNS Servers ............................. : {0}", dns.ToString());
                    }
                    Console.WriteLine();
                }*/

            }
        }

        #endregion

        #region Events

        private void ApplicationForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This callback is triggered when a checkbox's state changes in the NIC list box
        /// When the box is checked, the target NIC's DNS settings are changed to 127.0.0.1
        /// When the box is unchecked, the settings are restored to their original value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DNSlisbox_itemcheck_statechanged(object sender, ItemCheckEventArgs e)
        {
            if (this.shouldIgnoreCheckEvent != true)
            {
                try
                {

                    //Restore the user's old settings
                    if (e.CurrentValue == CheckState.Checked)
                    {
                        if (((NetworkListItem)DNSlistbox.Items[e.Index]).IPv4)
                        {
                            List<string> DNSservers = dnsServerslist[((NetworkListItem)DNSlistbox.Items[e.Index]).Id];
                            NetworkManager.setDNS(((NetworkListItem)DNSlistbox.Items[e.Index]).NICDescription, DNSservers);
                        }

                        //Since we don't set an IPv6 server, don't run this if they didn't have any to begin with
                        if (((NetworkListItem)DNSlistbox.Items[e.Index]).DNSserversv6.Count > 0)
                        {
                            List<string> DNSserversv6 = dnsServerslist[((NetworkListItem)DNSlistbox.Items[e.Index]).Id];
                            NetworkManager.setDNSv6(((NetworkListItem)DNSlistbox.Items[e.Index]).NICName, DNSserversv6);
                        }
                    }
                    else //Set it to loopback for DNSCrypt
                    {
                        if (((NetworkListItem)DNSlistbox.Items[e.Index]).IPv4)
                        {
                            NetworkManager.setDNS(((NetworkListItem)DNSlistbox.Items[e.Index]).NICDescription, "127.0.0.1");
                        }

                        //Only do this if there were custom servers set to begin with
                        if (((NetworkListItem)DNSlistbox.Items[e.Index]).DNSserversv6.Count > 0)
                        {
                            List<string> ip = new List<string>();
                            //ip.Add("::1");
                            NetworkManager.setDNSv6(((NetworkListItem)DNSlistbox.Items[e.Index]).NICName, ip);
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("There was an error changing the device's DNS server: " + exception.Message);
                }
                this.GetNICs(this.hideAdaptersCheckbox.Checked, true);
            }
        }
        
        private void revert_dns()
        {
            //Revert all of the DNS server settings
            foreach (NetworkListItem item in this.DNSlistbox.CheckedItems)
            {
                try
                {
                    List<string> DNSservers = dnsServerslist[item.Id];
                    NetworkManager.setDNS(item.NICDescription, DNSservers);

                    if (item.DNSserversv6.Count > 0)
                    {
                        List<string> DNSserversv6 = dnsServerslist[item.Id];
                        NetworkManager.setDNSv6(item.NICName, DNSserversv6);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("There was an error reverting your DNS settings: " + exception.Message);
                }
            }
            //this.DNSlistbox.Items.Clear();
            this.GetNICs(this.hideAdaptersCheckbox.Checked,true);
        }

		/// <summary>
		/// Called right before the form closes.
		/// Resets all DNS information back to their previous values and stops the DNSCrypt Proxy application
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void form_closing(object sender, FormClosingEventArgs e)
		{
            revert_dns();

			//Kill the DNSCrypt process if it's running
			if (this.CryptProcRunning && !this.CryptHandle.HasExited)
			{
				this.CryptHandle.Kill();
				this.CryptProc = null;

				this.service_button.Text = "Start";
				this.service_label.Text = "DNSCrypt is NOT running";
				this.CryptProcRunning = false;
			}
		}

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		/// <summary>
		/// The callback for double clicking the system tray icon
		/// Restores the main application window and shows the DNSCrypt Proxy application in a minimized state
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
			this.ShowInTaskbar = true;
			this.notifyIcon.Visible = false;

			if (this.CryptProcRunning)
			{
				ShowWindow(this.CryptHandle.MainWindowHandle, SW_SHOWMINIMIZED);
			}
		}

		/// <summary>
		/// The callback for clicking "Open" on the system tray
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void notifyIcon_Open(object sender, EventArgs e)
		{
			this.notifyIcon_MouseDoubleClick(sender, null);
		}

		/// <summary>
		/// Callback for when the window size is modified. Used to hijack the minimize action to place it in the system tray
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void form_resized(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				this.Hide();
				this.ShowInTaskbar = false;
				this.notifyIcon.Visible = true;

			//	if (this.CryptProcRunning)
			//	{
			//		ShowWindow(this.CryptHandle.MainWindowHandle, SW_HIDE);
			//	}
			}
	      }	

		/// <summary>
		/// The callback for clicking "Exit" on the system tray
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void notifyIcon_Exit(object sender, EventArgs e)
		{
            if (!this.CryptHandle.HasExited)
            {
                this.CryptHandle.Kill();
            }

            this.CryptProc = null;

            this.CryptProcRunning = false;

			this.Close();
		}

		/// <summary>
		/// Refreshes the NIC listing box with the hidden devices either shown or hidden
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshNICList(object sender, EventArgs e)
		{
            this.shouldIgnoreCheckEvent = true;
			this.DNSlistbox.Items.Clear();
			this.GetNICs(this.hideAdaptersCheckbox.Checked,false);
            this.shouldIgnoreCheckEvent = false;
		}

        private void serverBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.serverBox.SelectedItem.ToString() == "2.CloudNS.com.au    [Canberra, Australia]" | this.serverBox.SelectedItem.ToString() == "3.CloudNS.com.au    [Sydney, Australia]" | this.serverBox.SelectedItem.ToString() == "4.OpenNIC                [Japan]" | this.serverBox.SelectedItem.ToString() == "5.DNSCrypt.eu          [Holland]")
            {
                this.parentalControlsRadio.Enabled = false;
            }
            else
            {
                this.parentalControlsRadio.Enabled = true;
            }


            if (this.serverBox.SelectedItem.ToString() == "4.OpenNIC                [Japan]" | this.serverBox.SelectedItem.ToString() == "5.DNSCrypt.eu          [Holland]")
            {
                this.ipv6Radio.Enabled = false;
            }
            else
            {
                this.ipv6Radio.Enabled = true;
            }
        }

        #endregion

        #region  Something

      //---- timer to check proxy application------------
        private void tmr_Tick(object sender, EventArgs e)
        {
            // Make sure the proxy wasn't terminated by another application/user
            if (this.CryptHandle.HasExited==true)
            {
              // this.CryptHandle.Kill();  
                this.CryptProc = null;
                this.service_button.Text = "Start";
                status_update (0);
                this.CryptProcRunning = false;
                this.gatewayCheckbox.Enabled = true;
                this.ipv4Radio.Enabled = true;
                this.ipv6Radio.Enabled = true;
                this.serverBox.Enabled = true;
                this.protoTCP.Enabled = true;
                this.protoUDP.Enabled = true;
                this.portBox.Enabled = true;
                this.DNSlistbox.Enabled = true;
                this.parentalControlsRadio.Enabled = true;
                this.hideAdaptersCheckbox.Enabled = true;
                tmr.Enabled = false;
            }
        }
        private void check_proxy_dns(string s)
        {
            if (s != null)
            {
                if (s.StartsWith("[INFO] Done"))
                {
                    status_update(1);
                }
                if (s.StartsWith("[INFO] Server key fingerprint is"))
                {
                    status_update(2);
                }

                if (s.StartsWith("[INFO] Server key fingerprint is"))
                {
                    status_update(3);
                }

                if (s.StartsWith("[INFO] Proxying from"))
                {
                    status_update(4);
                }

                if (s.StartsWith("[ERROR] Unable to retrieve server certificates"))
                {
                    status_update(5);
                }
                
                if (s.StartsWith ("[INFO] Refetching server certificates"))
                {
                    status_update(6);
                }
            }
        }
        private void status_update(int s)
        {
        switch (s)
            {
            case  0:
                this.service_label.BackColor = Color.DarkRed;
                Appendlabel("DNSCrypt is NOT running");
                break;
            case 1:
                service_label.BackColor = Color.DarkTurquoise ;
                Appendlabel( "Connecting...");
                break;
            case 2:
                service_label.BackColor = Color.DarkTurquoise;
                Appendlabel("Generating a new key pair...");
                break;
            case 3:
                this.service_label.BackColor = Color.DarkKhaki;
                Appendlabel("certificate received and looks valid");
                break;
            case 4:
                this.service_label.BackColor = Color.DarkGreen;
                Appendlabel("DNSCrypt is running");
                break;
            case 5:
             this.service_label.BackColor = Color.DarkOrange ;
                Appendlabel("[ERROR] Unable to retrieve server certificates");
                break;
            case 6:
                this.service_label.BackColor = Color.DarkTurquoise;
                Appendlabel("Refetching server certificates");
                break;
            }
        }
        private void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            check_proxy_dns(e.Data);
            AppendTextBox(e.Data + "\r\n");

        }
        private void Appendlabel(string value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(Appendlabel), new object[] { value });
                return;
            }
            service_label.Text = value;
        }
        private void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            textBox1.Text += value;
        }

        #endregion


  
    }



    //DNS CLASS------------------------
   
    #region DNS On NIC Class
    /// <summary>
	/// Custom list item entry for the DNS list box. Allows us to store information about the NIC device.
	/// </summary>
	public class NetworkListItem
	{
        public String Id;
		public String NICName;
		public String NICDescription;
		public List<string> DNSservers;
		public List<string> DNSserversv6;
		public Boolean IPv4 = false;
		public Boolean IPv6 = false;
		public Boolean hidden = false;

		public NetworkListItem(String Id, String Name, String Description, List<string> IPs, List<string> IPsv6, Boolean IPv4, Boolean IPv6)
		{
            this.Id = Id;
			this.NICName = Name;
			this.NICDescription = Description;
			this.DNSservers = IPs;
			this.DNSserversv6 = IPsv6;
			this.IPv4 = IPv4;
			this.IPv6 = IPv6;

			if (this.shouldHide(Description))
			{
				this.hidden = true;
			}
		}

		public override string ToString()
		{
			String message = this.NICDescription + " - ";

			if (this.IPv4)
			{
				message += "IPv4: ";
				if (this.DNSservers.Count > 0)
				{
					message += String.Join(", ", DNSservers.ToArray());
				}
				else
				{
					message += "(Automatic)";
				}
			}

			if (this.IPv6)
			{
				message += " IPv6: ";
				if (this.DNSserversv6.Count > 0)
				{
					message += String.Join(", ", DNSserversv6.ToArray());
				}
				else
				{
					message += "(Automatic)";
				}
			}

			return message;
		}

		/// <summary>
		/// Returns if a device should be flagged as hidden or not.
		/// Adapters such as Hamachi or virtual machines typically have their own settings
		/// which you should rarely ever need to change.
		/// </summary>
		/// <param name="Name">The name of the NIC</param>
		private Boolean shouldHide(String Description)
		{
			string[] blacklist = { 
				"Microsoft Virtual",
				"Hamachi Network",
				"VMware Virtual",
				"VirtualBox",
				"Software Loopback",
				"Microsoft ISATAP",
				"Teredo Tunneling Pseudo-Interface"
			};

			foreach (string entry in blacklist)
			{
				if (Description.Contains(entry))
				{
					return true;
				}
			}

			return false;
		}
	}
#endregion

    //-------------------------------
}
