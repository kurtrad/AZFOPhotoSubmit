using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Xml;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace AZFOPhotoSubmit
{
    public class ABCSubmit
    {
        StreamWriter _sw = null;
        Html32TextWriter _doc = null;
        string _speciesName = string.Empty;
        string _FirstName = string.Empty;
        string _LastName = string.Empty;
        string _date = string.Empty;
        DateTime _dateTimeObserved = DateTime.Now;
        string _bandCode = string.Empty;
        string _htmlFileName = string.Empty;
        string _locationName = string.Empty;
        string _url = string.Empty;
        string _sPath = string.Empty;
        string _imagePath = string.Empty;
        string _imagesFolderPath = string.Empty;
        string _htmlFolderPath = string.Empty;
        bool _sendMail = false;
        

        public ABCSubmit(string locationName, string FirstName, string LastName, DateTime date, string bandCode, string speciesName)
        {
            _speciesName = speciesName;
            _locationName = locationName;
            _FirstName = FirstName;
            _LastName = LastName;
            _dateTimeObserved = date;
            _date = date.ToString("dd_MMMM_yyyy");
            _bandCode = bandCode;
            _htmlFileName = generateFileName(locationName, LastName, _date, bandCode);

            getFileLocationFromXML();

            _url = string.Format(@"http://www.azfo.org/ArizonaBirdReview/SubmittedHtml/{0}.html", _htmlFileName);
            //_sPath = string.Format(@"D:\websites\207.97.194.114\azfo.org\ArizonaBirdReview\submittedHTML\{0}.html", _htmlFileName);
            //_imagePath = string.Format(@"D:\websites\207.97.194.114\azfo.org\ArizonaBirdReview\submittedImages\");


            _sPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"ArizonaBirdReview\submittedHTML\{0}.html", _htmlFileName)));
            _imagePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ArizonaBirdReview\submittedImages\"));


            //// for testing only comment out when sending to server
            //_sPath = string.Format(@"C:\Users\radamaker\Documents\My stuff\WebPages\AZFONet\web-content\ArizonaBirdReview\submittedHTML\{0}.html", _htmlFileName);
            //_imagePath = string.Format(@"C:\Users\radamaker\Documents\My stuff\WebPages\AZFONet\web-content\ArizonaBirdReview\submittedImages\");
            //// for testing only comment out when sending to server

            _sw = null;
            _doc = null;

        }
        public ABCSubmit()
        {

        }

        //        [XML]
        //<Names>
        //    <Name>
        //        <FirstName>John</FirstName>
        //        <LastName>Smith</LastName>
        //    </Name>
        //    <Name>
        //        <FirstName>James</FirstName>
        //        <LastName>White</LastName>
        //    </Name>
        //</Names>

        private void getFileLocationFromXML()
        {
            string myXmlString = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/FileLocations.xml");

            XmlDocument xml = new XmlDocument();
            xml.Load(myXmlString); // suppose that myXmlString contains "<Names>...</Names>"

            XmlNodeList xnList = xml.SelectNodes("/fileLocations/ABCFileLocations");
            foreach (XmlNode xn in xnList)
            {
                _imagesFolderPath = xn["imagePath"].InnerText;
                _htmlFolderPath = xn["htmlPath"].InnerText;
            }

        }


        public string generateFileName(string locationName, string LastName, string _date, string bandCode)
        {
            // bandcode_location_name_date
            string filename = string.Format("{0}_{1}_{2}_{3}", bandCode, locationName, LastName, _date);
            return filename;
        }

        public void writeABCInfo()
        {
            _doc.Write(@"<p> 
							This record has been submitted to the ABC.</P>");
        }

        public void createWriters()
        {
            _sw = new StreamWriter(_sPath);
            _doc = new Html32TextWriter(_sw);
        }

        public void writeHeader()
        {
            _doc.Write("<html>" + '\n');
            _doc.Write("<head>" + '\n');
            _doc.Write("<meta http-equiv='content-type' content='text/html;charset=iso-8859-1'>" + '\n');
            _doc.Write(string.Format("<title>{0}</title>",_speciesName) + '\n');
            _doc.Write("<link href='../../style/main.css' rel='stylesheet' media='screen'>" + '\n');
            _doc.Write("<style type='text/css' media='all'></style>" + '\n');
            _doc.Write("<body leftmargin='0' marginheight='0' marginwidth='0' topmargin='0'>" + '\n');
            _doc.Write("<table width='797' border='0' cellspacing='0' cellpadding='0' align='center'>" + '\n');
            _doc.Write("<tr>" + '\n');
            _doc.Write("<td align='left' valign='bottom'><img src='../../ArizonaBirdCommittee/Resources/az_sta3.gif' alt='' border='0'></td>" + '\n');
            _doc.Write("</tr>" + '\n');
            _doc.Write("<tr>" + '\n');
            _doc.Write("<td class='background' align='left' valign='top'><img src='../../images/pixclear.gif' width='775' height='6' alt='' border='0'></td>" + '\n');
            _doc.Write("</tr>" + '\n');
            _doc.Write("<tr>" + '\n');
            _doc.Write("<td valign='top'><img src='../../images/pixclear.gif' width='395' height='1' alt='' border='0'></td>" + '\n');
            _doc.Write("</tr>" + '\n');
            _doc.Write("<tr valign='middle'>" + '\n');
            _doc.Write("<td valign='top'></td>" + '\n');
            _doc.Write("</tr>" + '\n');
            _doc.Write("</div></td></tr>" + '\n');
            
        }
		
        public void writeFooter()
        {
            _doc.Write("			<tr>" + '\n');
            _doc.Write("						<td align='left' valign='top' width='700'>" + '\n');
            _doc.Write(string.Format("						<p><i>Submitted on {0}</i></p>", DateTime.Today.ToString("dd MMMM yyyy")) + '\n');
            _doc.Write("				</td>" + '\n');
            _doc.Write("			</tr>" + '\n');
            _doc.Write("			<tr>" + '\n');
            _doc.Write("				<td align='left' valign='top' class='background' width='700'><img src='../../../images/pixclear.gif' width='775' height='6' alt='' border='0'></td>" + '\n');
            _doc.Write("			</tr>" + '\n');
            _doc.Write("			<tr>" + '\n');
            _doc.Write("				<td align='left' valign='top' width='700'><csobj occur='78' w='778' h='15' t='Component' csref='../../../web-data/Components/botnav.html'>" + '\n');
            _doc.Write("						<table width='749' border='0' cellspacing='0' cellpadding='0'>" + '\n');
            _doc.Write("							<tr>" + '\n');
            _doc.Write("								<td align='left' width='10'><img src='../../../images/pixclear.gif' width='10' height='1' alt='' border='0'></td>" + '\n');
            _doc.Write("								<td align='center' width='38'>" + '\n');
            _doc.Write("									<div align='left'>" + '\n');
            _doc.Write("										<span class='copyr'>&copy;2005</span></div>" + '\n');
            _doc.Write("								</td>" + '\n');
            _doc.Write("								<td align='center' width='38'><a href='../../index.html' class='botnav'>HOME</a></td>" + '\n');
            _doc.Write("								<td align='center' width='6'><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td align='center' width='106'><a href='../../reports.html' class='botnav'>REPORT SIGHTINGS</a></td>" + '\n');
            _doc.Write("								<td align='center' width='5' ><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td align='center' width='54'><a href='../../gallery/photos.html' class='botnav'>PHOTOS</a></td>" + '\n');
            _doc.Write("								<td align='center'  width='6'><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td align='center' width='51'><a href='../../birding/locations.html' class = 'botnav'>BIRDING</a></td>" + '\n');
            _doc.Write("								<td align='center' width='6'><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td width='62' align='center'><a href='../../journal/articles.html' class='botnav'>JOURNAL</a></td>" + '\n');
            _doc.Write("								<td align='center'  width='6'><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td width='65' align='center'><a href='../../contact.html' class='botnav'>ABOUT US</a></td>" + '\n');
            _doc.Write("								<td width='6' align='center'>|</td>" + '\n');
            _doc.Write("								<td width='82' align='center'><a href='../../checklist.html' class='botnav'>CHECKLISTS</a></td>" + '\n');
            _doc.Write("								<td width='6' align='center'>|</td>" + '\n');
            _doc.Write("								<td width='107' align='center'><a href='../../ArizonaBirdCommittee/index.html' class='botnav'>AZ BIRD COMMITTEE</a></td>" + '\n');
            _doc.Write("								<td width='6' align='center'>|</td>" + '\n');
            _doc.Write("								<td align='center' width='49'><a href='../../events/custom/eventslist.html'class='botnav'>EVENTS</a></td>" + '\n');
            _doc.Write("								<td align='center' width='6'>|</td>" + '\n');
            _doc.Write("								<td align='center'><a href='../../links.html' class='botnav'> LINKS</a></td>" + '\n');
            _doc.Write("							</tr>" + '\n');
            _doc.Write("						</table>" + '\n');
            _doc.Write("					</csobj></td>" + '\n');
            _doc.Write("			</tr>" + '\n');
            _doc.Write("		</table>" + '\n');
            _doc.Write("	</body>" + '\n');

            _doc.Write("</html>" + '\n');

        }

        public void writeReturnLinks()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void writeName(string submitInfo)
        {
            _doc.Write(submitInfo);
        }

        public void writeNameLocation(string nameAndLocation)
        {
            _doc.Write("			<tr>" + '\n');
            _doc.Write("				<td align='left' valign='top' width='700'>" + '\n');
            _doc.Write("					<div>" + '\n');
            _doc.Write(nameAndLocation);
        }

        public void writeImageLocations(string image1, string image2, string image3, string image4)
        {
            _doc.Write("			<tr>" + '\n');
            _doc.Write("			<td>" + '\n');
            _doc.Write(image1);
            _doc.Write("						<br>" + '\n');
            _doc.Write(image2);
            _doc.Write("						<br>" + '\n');
            _doc.Write(image3);
            _doc.Write("						<br>" + '\n');
            _doc.Write(image4);
            _doc.Write("						<br>" + '\n');
            _doc.Write("			</td>" + '\n');
            _doc.Write("			</tr>" + '\n');
        }

        public void writeABCDocumentation(string documentation)
        {
            _doc.Write("						<br>" + '\n');
            _doc.Write("<tr>" + '\n');
            _doc.Write("<p>" + '\n');
            _doc.Write("</p>" + '\n');
            _doc.Write("<td>" + '\n');
            _doc.Write(documentation);
            _doc.Write("						<br>" + '\n');
            _doc.Write("</td>" + '\n');
            _doc.Write("</tr>" + '\n');
        }

        public void insertDataRecord()
        {
            String strSQL;
            String today = DateTime.Now.ToString("yyMMdd_HHmmss");

            String record = generateFileName(_locationName, _LastName, today, _bandCode);

            // insert the header record
            // Get the conectionStrings section.

            ConnectionStringSettings cts = ConfigurationManager.ConnectionStrings["ABCProject1ConnectionString"];
            SqlDataSource sds = new SqlDataSource();

            // set the SQL string
            strSQL = "INSERT INTO _ABCReports (record, species , date_submitted, date_observed, url1, location, status ) " +
            String.Format("VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' )", record, _speciesName, DateTime.Now, _dateTimeObserved, _url, _locationName, "Submitted");

            try
            {

                sds.ConnectionString = cts.ConnectionString;
                sds.ProviderName = cts.ProviderName;
                sds.InsertCommand = strSQL;
                sds.Insert();
            }
            catch (Exception e)
            { 

            }
            finally
            {
                sds.Dispose();
            }     
           
            
        }


        public void closeFiles()
        {
            // close the files
            _doc.Close();
            _sw.Close();
        }

        public void SendMail(string emailAddressOfSubmitter, string formattedMessage)
        {
            //message.Body = @"Using this new feature, you can send an e-mail message from an application very easily.";
            MailMessage message = emailAddressInformation();

            if (_sendMail)
            {
                message.Body = formattedMessage + '\n' + "Url:" + _url + '\n';
                message.CC.Add(emailAddressOfSubmitter);
                message.IsBodyHtml = true;

                // Use the application or machine configuration to get the 
                // host, port, and credentials.

                SmtpClient client = new SmtpClient();
                client.Host = "server";

                //client.Host = "mail.azfo.org";
                client.Host = ConfigurationManager.AppSettings.Get("Mailhost");
                string username = ConfigurationManager.AppSettings.Get("Mailname");
                string pass = ConfigurationManager.AppSettings.Get("Mailpass");
                client.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential(username, pass);
                client.Credentials = cred;
                client.Send(message);
            }
            message.Dispose();

        }

        private MailMessage emailAddressInformation()
        {
            XmlDocument doc = new XmlDocument();
            string fileName = HttpContext.Current.Server.MapPath("~/App_Data/email.xml");
            doc.Load(fileName);
            //doc.Load(AppDomain.CurrentDomain)
            XmlNodeList nodes = null;
            MailAddress fromAddress = new MailAddress(doc.SelectSingleNode("email/ABCEmailList/from").InnerXml);
            MailMessage message = new MailMessage();

            message.Subject = "ABC Report of" + " " + _speciesName + " " + "observed on " + _date
                    + " by " + _FirstName + " " + _LastName;

            // add the TO list of emails
            nodes = doc.SelectNodes("email/ABCEmailList/to");
            foreach (XmlNode toNode in nodes)
            {
                message.To.Add(toNode.InnerText);
            }

            // add the CC list of emails
            nodes = doc.SelectNodes("email/ABCEmailList/cc");
            foreach (XmlNode ccNode in nodes)
            {
                message.CC.Add(ccNode.InnerText);
            }

            message.From = fromAddress;
            _sendMail = Convert.ToBoolean(doc.SelectSingleNode("email/ABCEmailList/send").InnerXml);

            // message.Attachments.Add(new Attachment(FileBrowse.Value)); //Adding attachment to the mail.
            // string sPath = @"D:\websites\69.20.75.56\azfo.org.com\temp\";

            return message;
        }

        #region gets
        public string getImagePath
        {
            get { return _imagePath; }
        }

        public string getFileName
        {
            get { return _htmlFileName; }
        }

        #endregion
    }


}
