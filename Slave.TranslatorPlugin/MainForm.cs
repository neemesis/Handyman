using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Slave.TranslatorPlugin
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class MainForm : Form
	{		
				
		public MainForm()
		{
			InitializeComponent();			
		}		

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			
			uxLpComboBox.ValueMember = "Code";
			uxLpComboBox.DisplayMember = "Caption";
			uxLpComboBox.DataSource = LanguagePair.GoogleLanguagePairs;

			uxLpComboBox.DataBindings.Add("SelectedValue", Properties.Settings.Default, "SelectedLanguage", true, DataSourceUpdateMode.OnPropertyChanged);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			Properties.Settings.Default.Save();
		}

		//private void Form1_Load(object sender, System.EventArgs e)
		//{
		//    //IDataObject datas =  Clipboard.GetDataObject();
		//    //string clip = (string)datas.GetData(DataFormats.Text);
		//    //MessageBox.Show(clip);
		//}

		private void button1_Click(object sender, EventArgs e)
		{
			uxTranslateButton.Enabled = false;
			toolStripProgressBar1.Visible = true;
			toolStripStatusLabel1.Text = "Translating...";
			uxBackgroundWorker.RunWorkerAsync(uxLpComboBox.SelectedValue + "$" + textBox1.Text.Replace("\r\n", string.Empty));
		}

		private void uxBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var input = e.Argument.ToString();

			var google = ReadHtmlPage("http://translate.google.com/translate_t", "text=" + input.Split('$')[1] + "&langpair=" + input.Split('$')[0] + "&ie=UTF8&oe=UTF8");

			var regex = "<div id=result_box dir=ltr>([^/<]*)</div>";
			var options = ((System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.Multiline) | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			var reg = new System.Text.RegularExpressions.Regex(regex, options);

			e.Result  = reg.Match(google).Groups[1].Value;
		}

		private void uxBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			toolStripProgressBar1.Visible = false;
			toolStripStatusLabel1.Text = "Ready";
			textBox2.Text = e.Result.ToString();
			uxTranslateButton.Enabled = true;
		}

		private String ReadHtmlPage(string url, string postDatas)
		{
			var result = string.Empty;
			var strPost = postDatas; // "key=value&key1=value2"
			Stream stream = null;

			var encoding = new System.Text.UTF8Encoding();

			var objRequest =	(HttpWebRequest)WebRequest.Create(url);
			objRequest.CookieContainer = new CookieContainer();
			objRequest.Method = "POST";
			objRequest.ContentType = "application/x-www-form-urlencoded";

			var byte1 = encoding.GetBytes(postDatas);
			objRequest.ContentLength = byte1.Length;

			stream = objRequest.GetRequestStream();
			stream.Write(byte1, 0, byte1.Length);
			stream.Close();
						

			var objResponse = (HttpWebResponse)objRequest.GetResponse();
			var cookies = objResponse.Cookies;

			System.Text.StringBuilder sb = null;
			if (objResponse.ContentLength > 0)
			{
				sb = new System.Text.StringBuilder((int)objResponse.ContentLength);
			}
			else
			{
				sb = new System.Text.StringBuilder();
			}
			using (var str = objResponse.GetResponseStream())
			{
				using (var reader = new StreamReader(str, true))
				{
					var bufferSize = 1024;
					var buffer = new char[bufferSize];
					var pos = 0;
					while ((pos = reader.Read(buffer, 0, bufferSize)) > 0)
					{
						sb.Append(buffer, 0, pos);
					}
					// content = reader.ReadToEnd();
					reader.Close();
				}
			}

			if (objResponse != null)
			{
				objResponse.Close();
				objResponse = null;
			}

			//byte1 = new Byte[objResponse.ContentLength];
			//stream = objResponse.GetResponseStream();
			//stream.Read(byte1, 0, (int)objResponse.ContentLength);
			//stream.Close();
			//objResponse.Close();
			//result = encoding.GetString(byte1);
			return sb.ToString();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		//private void textBox1_KeyUp(object sender, KeyEventArgs e)
		//{
		//    if (e.KeyCode == Keys.Enter)
		//    {
		//        button1_Click(sender, EventArgs.Empty);
		//    }
		//}

		private void uxFrenchToEnglishToolStripButton_Click(object sender, EventArgs e)
		{
			uxLpComboBox.SelectedValue = "fr|en";
		}

		private void uxEnglishToFrenchToolStripButton_Click(object sender, EventArgs e)
		{
			uxLpComboBox.SelectedValue = "en|fr";
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Simple Google based Translator tool.", "SerialCoder 2006");
		}

		
		private void uxLpComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selection = uxLpComboBox.SelectedItem as LanguagePair;

			uxFrenchToEnglishToolStripButton.Checked = false;
			uxEnglishToFrenchToolStripButton.Checked = false;

			if (selection.Code == "fr|en")
			{
				uxFrenchToEnglishToolStripButton.Checked = true;
			}
			else if (selection.Code == "en|fr")
			{				
				uxEnglishToFrenchToolStripButton.Checked = true;
			}
		}

		private void textBox1_KeyUp(object sender, KeyEventArgs e)
		{
			 if (e.Control && e.KeyCode == Keys.Enter)
			 {
				 button1_Click(sender, EventArgs.Empty);
			 }
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.Save();
		}
	}
}
