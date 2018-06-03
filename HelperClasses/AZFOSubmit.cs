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

namespace AZFOPhotoSubmit
{
    public class AZFOSubmit
    {
        StreamWriter _sw = null;
        Html32TextWriter _doc = null;
        string _speciesName = string.Empty;
        string _FirstName = string.Empty;
        string _htmlFileName = string.Empty;
        string _url = string.Empty;
        string _sPath = string.Empty;
        string _imagePath = string.Empty;
        bool _sendMail = false;
        DateTime _dateTimeObserved = DateTime.Now;
        string _locationName, _LastName, _bandCode = string.Empty;

        public AZFOSubmit(string locationName, string FirstName, string LastName, string date, string bandCode, string speciesName)
        {
            _locationName = locationName;
            _LastName = LastName;
            _bandCode = bandCode;
            _speciesName = speciesName;
            _FirstName = FirstName;
            _htmlFileName = generateFileName(locationName, LastName, date, bandCode);
            _url = string.Format(@"http://www.azfo.org/gallery/photoSubmit/SubmittedHtml/{0}.html", _htmlFileName);
            //_sPath = string.Format(@"D:\websites\207.97.194.114\azfo.org\gallery\photoSubmit\submittedHTML\{0}.html", _htmlFileName);
            //_imagePath = string.Format(@"D:\websites\207.97.194.114\azfo.org\gallery\photoSubmit\submittedImages\");

            //_sPath = string.Format(@"D:\websites\207.97.194.114\azfo.org\gallery\photoSubmit\submittedHTML\{0}.html", _htmlFileName);
            //_imagePath = string.Format(@"D:\websites\207.97.194.114\azfo.org\gallery\photoSubmit\submittedImages\");

            _sPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"gallery\photoSubmit\submittedHTML\{0}.html", _htmlFileName)));
            _imagePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"gallery\photoSubmit\submittedImages\"));

#if DEBUG
            // for testing only comment out when sending to server
            //_sPath = string.Format(@"C:\Users\radamaker\Documents\My stuff\WebPages\AZFONet\web-content\gallery\photoSubmit\submittedHTML\{0}.html", _htmlFileName);
            //_imagePath = string.Format(@"C:\Users\radamaker\Documents\My stuff\WebPages\AZFONet\web-content\gallery\photoSubmit\submittedImages\");
            // for testing only comment out when sending to server
#endif
            _sw = null;
            _doc = null;

        }

        public string generateFileName(string locationName, string LastName, string date, string bandCode)
        {
            // bandcode_location_name_date
            string filename = string.Format("{0}_{1}_{2}_{3}", bandCode, locationName, LastName, date);
            return filename;
        }

        public void createWriters()
        {
            _sw = new StreamWriter(_sPath);
            _doc = new Html32TextWriter(_sw);
        }

        public void writeABCInfo()
        {
            //_doc.Write("<p>Submitting photos to AZFO does not constitute reporting a rare bird for consideration as an official record.  To ensure that a record becomes official, details or photos of Sketch Details Species should be sent to the editors of North American Birds here and a full report of Review Species should be submitted to the Arizona Bird Committee here.  Most rare birds are under _documented and subsequent observers of a rarity are always encouraged to submit reports and additional photos, especially for birds that are challenging to identify.</p>");
            _doc.Write(@"<p> 
							Submitting photos to AZFO does not constitute reporting a rare bird for 
							consideration as an official record. To ensure that 
							a record becomes official, details or photos of 
							Sketch Details Species should be sent to the
							<a href='http://www.azfo.org/gallery/NABEditors.html'>Editors of North American Birds</a> and a full report of Review Species should be 
							submitted to the 
							<a href='http://www.azfo.org/gallery/whatIsABC.html'>Arizona Bird Committee</a>. Most 
							rare birds are under _documented and subsequent 
							observers of a rarity are always encouraged to 
							submit reports and additional photos, especially for 
							birds that are challenging to identify.</P>");
        }

        public void writeHeader()
        {
            _doc.Write("<html>" + '\n');
            _doc.Write("<head>" + '\n');
            _doc.Write("		<meta http-equiv='content-type' content='text/html;charset=iso-8859-1'>				" + '\n');
            _doc.Write(string.Format("		<title>{0}</title>													", _speciesName) + '\n');
            _doc.Write("		<script type='text/javascript' src='../allpages.js'></script>" + '\n');
            //_doc.Write("<a  id=\"" + this.UniqueID + "\" href=\"" + ClientScript.GetPostBackEventReference(this,null) +"\">");
            //_doc.Write(" " + this.UniqueID + "</a>");
            _doc.Write("		<link href='../../../style/main.css' rel='stylesheet' type='text/css' media='all'>" + '\n');
            _doc.Write("		<csimport user='../../../../web-data/Components/PhotoBanner.html' occur='52'>" + '\n');
            _doc.Write("			<csimport user='../../../../web-data/Components/topnav.html' occur='35'>" + '\n');
            _doc.Write("				<link href='../../../style/main.css' rel='stylesheet' media='screen'>" + '\n');
            _doc.Write("			</csimport>" + '\n');
            _doc.Write("		</csimport>" + '\n');
            _doc.Write("	</head>" + '\n');
            _doc.Write("	<body leftmargin='0' marginheight='0' marginwidth='0' topmargin='0'>" + '\n');
            _doc.Write("		<table width='700' border='0' cellspacing='0' cellpadding='0' align='center'>" + '\n');
            _doc.Write("			<tr height='75'>" + '\n');
            _doc.Write("				<td align='left' valign='bottom' width='700' height='75'><csobj occur='52' w='778' h='80' t='Component' csref='../../../web-data/Components/PhotoBanner.html'>" + '\n');
            _doc.Write("						<table width='775' border='0' cellspacing='0' cellpadding='0'>" + '\n');
            _doc.Write("							<tr>" + '\n');
            _doc.Write("								<td width='160'><a href='../index.html'><img src='../../../gallery/images/azfologo3.gif' border='0' livesrc='../../../web-data/SmartObjects/azfologo3.gif' width='160' height='65' alt='Arizona Field Ornithologist'></a></td>" + '\n');
            _doc.Write("								<td nowrap width='615'><img src='../../../images/PhotoGallery2.jpg' alt='' height='65' width='618' border='0' livesrc='../../../web-data/SmartObjects/PhotoGallery2.psd'></td>" + '\n');
            _doc.Write("							</tr>" + '\n');
            _doc.Write("							<tr>" + '\n');
            _doc.Write("								<td colspan='2' width='778'><csobj csref='../../../web-data/Components/topnav.html' h='15' occur='35' t='Component' w='778'>" + '\n');
            _doc.Write("										<table width='749' border='0' cellspacing='0' cellpadding='0'>" + '\n');
            _doc.Write("											<tr>" + '\n');
            _doc.Write("												<td width='10'><img src='../../../images/pixclear.gif' width='10' height='1' alt='' border='0'></td>" + '\n');
            _doc.Write("												<td width='38'>" + '\n');
            _doc.Write("													<div align='left'>" + '\n');
            _doc.Write("														<span class='copyr'>&copy;2008</span></div>" + '\n');
            _doc.Write("												</td>" + '\n');
            _doc.Write("												<td width='38'><a href='../../../index.html' class='topnav'>HOME</a></td>" + '\n');
            _doc.Write("												<td width='6'><span class='topnav'>|</span></td>" + '\n');
            _doc.Write("												<td width='106'><a href='../../../reports.html' class='topnav'>REPORT SIGHTINGS</a></td>" + '\n');
            _doc.Write("												<td width='5' ><span class='topnav'>|</span></td>" + '\n');
            _doc.Write("												<td width='54'><a href='../../../gallery/photos.html' class='topnav'>PHOTOS</a></td>" + '\n');
            _doc.Write("												<td  width='6'><span class='topnav'>|</span></td>" + '\n');
            _doc.Write("												<td width='51'><a href='../../../birding/locations.html' class = 'topnav'>BIRDING</a></td>" + '\n');
            _doc.Write("												<td width='6'><span class='topnav'>|</span></td>" + '\n');
            _doc.Write("												<td width='62'><a href='../../../journal/articles.html' class='topnav'>JOURNAL</a></td>" + '\n');
            _doc.Write("												<td  width='6'><span class='topnav'>|</span></td>" + '\n');
            _doc.Write("												<td width='65'><a href='../../../contact.html' class='topnav'>ABOUT US</a></td>" + '\n');
            _doc.Write("												<td width='6'>|</td>" + '\n');
            _doc.Write("												<td width='82'><a href='../../../checklist.html' class='topnav'>CHECKLISTS</a></td>" + '\n');
            _doc.Write("												<td width='6'>|</td>" + '\n');
            _doc.Write("												<td width='104'><a href='../../../namc/aznamc.html' class='topnav'>MIGRATION COUNT</a></td>" + '\n');
            _doc.Write("												<td width='6'>|</td>" + '\n');
            _doc.Write("												<td width='49'><a href='../../../events/custom/eventslist.html'class='topnav'>EVENTS</a></td>" + '\n');
            _doc.Write("												<td width='6'>|</td>" + '\n');
            _doc.Write("												<td><a href='../../../links.html' class='topnav'> LINKS</a></td>" + '\n');
            _doc.Write("											</tr>" + '\n');
            _doc.Write("										</table>" + '\n');
            _doc.Write("									</csobj></td>" + '\n');
            _doc.Write("							</tr>" + '\n');
            _doc.Write("						</table>" + '\n');
            _doc.Write("					</csobj></td>" + '\n');
            _doc.Write("			</tr>" + '\n');
            _doc.Write("			<tr>" + '\n');
            _doc.Write("				<td align='left' class='background' valign='top' width='700'><img src='../../../images/pixclear.gif' width='775' height='6' alt='' border='0'></td>" + '\n');
            _doc.Write("			</tr>" + '\n');
            _doc.Write("			<tr height='10'>" + '\n');
            _doc.Write("				<td valign='top' width='700'><img src='../../../images/pixclear.gif' width='395' height='6' alt='' border='0'></td>" + '\n');
            _doc.Write("			</tr>" + '\n');
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
            _doc.Write("								<td align='center' width='38'><a href='../../../index.html' class='botnav'>HOME</a></td>" + '\n');
            _doc.Write("								<td align='center' width='6'><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td align='center' width='106'><a href='../../../reports.html' class='botnav'>REPORT SIGHTINGS</a></td>" + '\n');
            _doc.Write("								<td align='center' width='5' ><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td align='center' width='54'><a href='../../photos.html' class='botnav'>PHOTOS</a></td>" + '\n');
            _doc.Write("								<td align='center'  width='6'><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td align='center' width='51'><a href='../../../birding/locations.html' class = 'botnav'>BIRDING</a></td>" + '\n');
            _doc.Write("								<td align='center' width='6'><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td width='62' align='center'><a href='../../../journal/articles.html' class='botnav'>JOURNAL</a></td>" + '\n');
            _doc.Write("								<td align='center'  width='6'><span class='botnav'>|</span></td>" + '\n');
            _doc.Write("								<td width='65' align='center'><a href='../../../contact.html' class='botnav'>ABOUT US</a></td>" + '\n');
            _doc.Write("								<td width='6' align='center'>|</td>" + '\n');
            _doc.Write("								<td width='82' align='center'><a href='../../../checklist.html' class='botnav'>CHECKLISTS</a></td>" + '\n');
            _doc.Write("								<td width='6' align='center'>|</td>" + '\n');
            _doc.Write("								<td width='107' align='center'><a href='../../../ArizonaBirdCommittee/index.html' class='botnav'>AZ BIRD COMMITTEE</a></td>" + '\n');
            _doc.Write("								<td width='6' align='center'>|</td>" + '\n');
            _doc.Write("								<td align='center' width='49'><a href='../../../events/custom/eventslist.html'class='botnav'>EVENTS</a></td>" + '\n');
            _doc.Write("								<td align='center' width='6'>|</td>" + '\n');
            _doc.Write("								<td align='center'><a href='../../../links.html' class='botnav'> LINKS</a></td>" + '\n');
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
            _doc.Write(image1);
            _doc.Write(image2);
            _doc.Write(image3);
            _doc.Write(image4);
        }

        public void writeCredit(string credit)
        {
            //_doc.Write("						<p><img src='../gallery/images/ABitternDitch1.jpg' alt='American Bittern, Gilbert, Richard Ditch 1' width='600' height='464' border='0' align='bottom'></p>" + '\n');
            _doc.Write("						<br>" + '\n');
            _doc.Write(credit);
            //_doc.Write("						<p><i>10 December 2005 photo by Kurt Radamaker</i></p>" + '\n');
            _doc.Write("						<b>All photos are copyrighted&copy; by photographer<br>" + '\n');
            _doc.Write("						<br>" + '\n');
            _doc.Write("					</div>" + '\n');
            _doc.Write("				</td>" + '\n');
            _doc.Write("			</tr>" + '\n');
        }

        public void writeIDPoints(string IDPoints)
        {
            _doc.Write(IDPoints);
        }

        public void writeStatus(string status)
        {
            _doc.Write(status);
        }

        public void writeNotes(string notes)
        {
            _doc.Write(notes);
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

        public void SendMail(string emailAddressOfSubmitter)
        {
            MailMessage message = emailAddressInformation();

            if (_sendMail)
            {
                message.Subject = string.Format("AZFO Photo Documentation of {0}, submitted {1}", _speciesName, DateTime.Now.ToString("dd MMMM yyyy"));

                message.Body = formatMessage();
                //message.Body = @"Using this new feature, you can send an e-mail message from an application very easily.";
                message.To.Add(emailAddressOfSubmitter);
                message.Bcc.Add("kurtrad@att.net");
                message.Bcc.Add("kurtrad@mexicobirding.com");

                // host, port, and credentials.

                SmtpClient client = new SmtpClient();
                //client.Host = "server";

                //client.Host = "mail.azfo.org";
                //client.UseDefaultCredentials = false;
                //NetworkCredential cred = new NetworkCredential("azfo@azfo.org", "AZFOis2936");
                ////NetworkCredential cred = new NetworkCredential("photoeditor@azfo.org", "azfophotos");
                //client.Credentials = cred;
                //client.Send(message);
                //message.Dispose();


                //client.Host = "mail.azfo.org";
                client.Host = ConfigurationManager.AppSettings.Get("Mailhost");
                client.Port = 587;
                client.Host = "mail.azfo.org";
                string username = ConfigurationManager.AppSettings.Get("Mailname");
                string pass = ConfigurationManager.AppSettings.Get("Mailpass");
                client.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential(username, pass);
                client.Credentials = cred;
                client.Send(message);
                message.Dispose();
            }
        }

        private MailMessage emailAddressInformation()
        {
            XmlDocument doc = new XmlDocument();
            string fileName = HttpContext.Current.Server.MapPath("~/App_Data/email.xml");
            doc.Load(fileName);
            XmlNodeList nodes = null;
            MailAddress fromAddress = new MailAddress(doc.SelectSingleNode("email/AZFOEmailList/from").InnerXml);
            MailMessage message = new MailMessage();

            // add the TO list of emails
            nodes = doc.SelectNodes("email/AZFOEmailList/to");
            foreach (XmlNode toNode in nodes)
            {
                message.To.Add(toNode.InnerText);
            }

            // add the CC list of emails
            nodes = doc.SelectNodes("email/AZFOEmailList/cc");
            foreach (XmlNode ccNode in nodes)
            {
                message.CC.Add(ccNode.InnerText);
            }

            message.From = fromAddress;
            _sendMail = Convert.ToBoolean(doc.SelectSingleNode("email/AZFOEmailList/send").InnerXml);

            // message.Attachments.Add(new Attachment(FileBrowse.Value)); //Adding attachment to the mail.
            // string sPath = @"D:\websites\69.20.75.56\azfo.org.com\temp\";

            return message;
        }

        //Format the Message Body
        //*******************************************************************************************************************
        public string formatMessage()
        {
            //string formattedMessage = "Thank you " + TextBoxFirstName.Text + " for your contribution." + '\n' + '\n' +
            //"Your contributions are greatly appreciated. AZFO will get back to you as soon as possible " +
            //"(remember we are volunteers!).  If your photos are selected for posting, the web page details and " +
            //"photos you submitted will be edited and embellished and possibly combined on a single page with other submissions. "
            //+ '\n'
            //+ '\n'
            //+ "Again thank you for your contribution" + '\n'
            //+ '\n'
            //+ '\n'
            //+ "A link to a mock-up of what your page may look like is here: " + '\n'
            //+ _url
            //+ '\n'
            //+ '\n'
            //+ "If you see any errors in the mock-up please email the editor with corrections" + '\n'
            //+ '\n'
            //+ '\n'
            //+ "AZFO Photo Editors.";

            string formattedMessage = string.Format(@"Thank you {0} for your contribution. Your contributions are greatly appreciated. AZFO will get back to you as soon as possible(remember we are volunteers!).  If your photos are selected for posting, the web page details and photos you submitted will be edited and embellished and possibly combined on a single page with other submissions.

Again thank you for your contribution.

A link to a mock-up of what your page may look like is here: 
{1}

If you see any errors in the mock-up please email the editor with corrections
			 
AZFO Photo Editors.", _FirstName, _url);

            return (formattedMessage);
        }


        #region gets
        public string getImagePath
        {
            get {return _imagePath;}
        }

        public string getFileName
        {
            get { return _htmlFileName; }
        }

        #endregion
    }

    
}
