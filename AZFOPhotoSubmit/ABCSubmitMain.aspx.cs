using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

namespace AZFOPhotoSubmit
{
    public partial class ABCSubmitMain : System.Web.UI.Page
    {
        ABCSubmit abcSubmit = null;
        string _nameAndLocation = string.Empty;
        string _image1 = string.Empty;
        string _image2 = string.Empty;
        string _image3 = string.Empty;
        string _image4 = string.Empty;
        string _urlReturn = @"http://www.azfo.org/gallery/photos.html";
        string speciesName = string.Empty;
        string _latinName = string.Empty;
        string _credits = string.Empty;
        string _bandCode = string.Empty;
        DateTime _dateOfPhoto = DateTime.Now;
        const string thankYou = @"http://www.azfo.org/gallery/photoSubmit/thankyou.html";
        bool _fileSave = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private string generateFileName()
        {
            string LastName = string.Empty;

            string locationName = filterForFileName(City.Value);

            if (TextBoxOnBehalfOf.Text != string.Empty)
                LastName = TextBoxOnBehalfOf.Text.Replace(' ', '_');
            else
                LastName = TextBoxLastName.Text.Replace(' ', '_');

            string date = _dateOfPhoto.ToString("yyyyMMdd");
            _bandCode = getSpeciesBandCode();
            // bandcode_location_name_date
            string filename = string.Format("{0}_{1}_{2}_{3}", _bandCode, locationName, LastName, date);
            return filename;
        }

        private string getSpeciesBandCode()
        {
            string bandcodeout = "bandcode";
            XmlDocument doc = new XmlDocument();
            doc = XmlDataSource1.GetXmlDocument();

            XmlNode node;
            XmlNode bandcode;
            XmlNode root = doc.DocumentElement;
            string xmlspeciesname = string.Empty;

            if (speciesName.Contains("'"))
                xmlspeciesname = buildXPathString(speciesName);
            else
                xmlspeciesname = "'" + speciesName + "'";

            node = root.SelectSingleNode(string.Format("Species[@EnglishName ={0}]", xmlspeciesname));
            bandcode = node.Attributes.GetNamedItem("BandCode");
            //Get the BandCode
            bandcodeout = bandcode.InnerText;
            return bandcodeout;
        }

        public static string buildXPathString(string input)
        {
            string[] components = input.Split(new char[] { '\'' });
            string result = "";
            result += "concat('";
            for (int i = 0; i < components.Length; i++)
            {
                if (i == 0)
                    result += components[i] + "'";
                else
                    result += ", '" + components[i] + "'";
                if (i < components.Length - 1)
                {
                    result += ", \"'\"";
                }
            }
            result += ")";
            return result;
        }


        private string filterForFileName(string fileNameNew)
        {
            // Replace invalid file name characters \ /:*?"<>| 
            fileNameNew = fileNameNew.Replace(":", "");
            fileNameNew = fileNameNew.Replace("*", "");
            fileNameNew = fileNameNew.Replace("?", "");
            fileNameNew = fileNameNew.Replace("<", "");
            fileNameNew = fileNameNew.Replace(">", "");
            fileNameNew = fileNameNew.Replace("|", "");
            fileNameNew = fileNameNew.Replace("/", "");
            fileNameNew = fileNameNew.Replace("\\", "");
            fileNameNew = fileNameNew.Replace("\"", "");
            fileNameNew = fileNameNew.Replace("'", "");
            fileNameNew = fileNameNew.Replace(" ", "_");
            fileNameNew = fileNameNew.Replace("$", "");
            fileNameNew = fileNameNew.Replace("%", "");
            fileNameNew = fileNameNew.Replace("^", "");
            fileNameNew = fileNameNew.Replace("@", "");
            fileNameNew = fileNameNew.Replace("#", "");
            fileNameNew = fileNameNew.Replace("&", "");
            fileNameNew = fileNameNew.Replace("(", "");
            fileNameNew = fileNameNew.Replace(")", "");

            return fileNameNew;
        }

        #region save ABC Files
        private void saveUpLoadedFilesABC()
        {
            string file1 = abcSubmit.getFileName;

            if (FileUpload1.FileName != string.Empty)
            {
                LabelFileType1.Visible = false;

                if (validateFileExtension(FileUpload1))
                {
                    // what kind of file is this
                    //FileInfo f = new FileInfo(FileUpload1.FileName);
                    //string extension = f.Extension;
                    string extension = Path.GetExtension(FileUpload1.FileName).ToLower();
                    string file1Name = string.Format("{0}_a{1}", file1, extension);
                    string filename1FullPath = string.Format("{0}{1}_a{2}", abcSubmit.getImagePath, file1, extension);

                    FileUpload1.SaveAs(filename1FullPath);
                    _image1 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='850'></p>", file1Name
                        );

                }
                else
                {
                    LabelFileType1.Text = "Only image files are accepted";
                    LabelFileType1.Visible = true;
                }
            }
            if (FileUpload2.FileName != string.Empty)
            {
                LabelFileType2.Visible = false;

                if (validateFileExtension(FileUpload2))
                {
                    // what kind of file is this
                    //FileInfo f = new FileInfo(FileUpload2.FileName);
                    //string extension = f.Extension;
                    string extension = Path.GetExtension(FileUpload2.FileName).ToLower();
                    string file2Name = string.Format("{0}_b{1}", file1, extension);
                    string filename2FullPath = string.Format("{0}{1}_b{2}", abcSubmit.getImagePath, file1, extension);

                    FileUpload2.SaveAs(filename2FullPath);
                    _image2 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='850'></p>", file2Name);

                }
                else
                {
                    LabelFileType2.Text = "Only image files are accepted";
                    LabelFileType2.Visible = true;
                }
            }
            if (FileUpload3.FileName != string.Empty)
            {
                LabelFileType3.Visible = false;

                if (validateFileExtension(FileUpload3))
                {
                    // what kind of file is this
                    //FileInfo f = new FileInfo(FileUpload3.FileName);
                    //string extension = f.Extension;
                    string extension = Path.GetExtension(FileUpload3.FileName).ToLower();
                    string file3Name = string.Format("{0}_c{1}", file1, extension);
                    string filename3FullPath = string.Format("{0}{1}_c{2}", abcSubmit.getImagePath, file1, extension);

                    FileUpload3.SaveAs(filename3FullPath);
                    _image3 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='850'></p>", file3Name);

                }
                else
                {
                    LabelFileType3.Text = "Only image files are accepted";
                    LabelFileType3.Visible = true;
                }
            }
            if (FileUpload4.FileName != string.Empty)
            {
                LabelFileType4.Visible = false;

                if (validateFileExtension(FileUpload4))
                {
                    // what kind of file is this
                    //FileInfo f = new FileInfo(FileUpload4.FileName);
                    //string extension = f.Extension;
                    string extension = Path.GetExtension(FileUpload4.FileName).ToLower();
                    string file4Name = string.Format("{0}_d{1}", file1, extension);
                    string filename4FullPath = string.Format("{0}{1}_d{2}", abcSubmit.getImagePath, file1, extension);

                    FileUpload4.SaveAs(filename4FullPath);
                    _image4 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='850'></p>", file4Name);

                }
                else
                {
                    LabelFileType4.Text = "Only image files are accepted";
                    LabelFileType4.Visible = true;
                }
            }

        }

        #endregion


        private bool validateFileExtension(FileUpload FileUploaded)
        {
            Boolean fileOK = false;
            if (FileUploaded.HasFile)
            {
                String fileExtension =
                    Path.GetExtension(FileUploaded.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            return fileOK;
        }

        protected void DropDownListOfSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListOfSpecies.SelectedIndex == 1)
            {
                TextBoxOther.Visible = true;
                LabelOther.Visible = true;
                RequiredFieldValidatorOther.Visible = true;
                speciesName = TextBoxOther.Text;
                _latinName = "Latin Name";
                _bandCode = speciesName.Replace(' ', '_');
            }
            else
            {
                TextBoxOther.Visible = false;
                LabelOther.Visible = false;
                RequiredFieldValidatorOther.Visible = false;
                speciesName = DropDownListOfSpecies.SelectedItem.Text;
                _bandCode = getSpeciesBandCode();

            }
            
        }

        protected void btnSend_Click1(object sender, EventArgs e)
        {
            //Event fires for sending the mail.
            //*******************************************************************************************************************
            bool sendit = true;

            //// no date selected
            //if (Calendar1.SelectedDate.Year == 0001)
            //{
            //    LabelValidDate.Visible = true;
            //    sendit = false;
            //}

            // if form entry is valid build the html files
            if (sendit)
                BuildABCHTML();


            // this temp to stop emails
            string email = NewformatABCHTMLText();

            bool pigscanfly = true;
            if (pigscanfly)
            {
                if (sendit)
                    abcSubmit.SendMail(TextBoxEmailAddress.Text, NewformatABCHTMLText());
            }

            if (sendit)
            {
                //lblMessage.ForeColor = Color.Navy;
                //lblMessage.Font.Size = 14;

                ////User information after submission.
                //lblMessage.Text = "Thank You, " + TextBoxFirstName.Text + " " + TextBoxLastName.Text + " - *Your email has been sent successfully*";

                Response.Redirect("thankyouABC.html");
            }
        }

        #region build ABC Html

        private void BuildABCHTML()
        {

            if (DropDownListOfSpecies.SelectedIndex == 1)
            {
                TextBoxOther.Visible = true;
                LabelOther.Visible = true;

                speciesName = filterForFileName(TextBoxOther.Text);
                _latinName = "Latin Name";
                _bandCode = speciesName.Replace(' ', '_');
            }
            else
            {
                TextBoxOther.Visible = false;
                LabelOther.Visible = false;
                speciesName = DropDownListOfSpecies.SelectedItem.Text;
                _bandCode = getSpeciesBandCode();

            }

            _latinName = DropDownListOfSpecies.SelectedItem.Value;
            string countyName = County.SelectedItem.Value;
            string FirstName = TextBoxFirstName.Text;
            string emailAddress = TextBoxEmailAddress.Text;
            string LastName = string.Empty;

            string locationName = filterForFileName(City.Value);
            if (TextBoxOnBehalfOf.Text != string.Empty)
            {
                LastName = TextBoxOnBehalfOf.Text.Replace(' ', '_');
                FirstName = string.Empty;
            }
            else
            {
                LastName = TextBoxLastName.Text.Replace(' ', '_');
            }

            //string date = string.Format("{0}{1}{2}", _dateOfPhoto.Year, _dateOfPhoto.Month, _dateOfPhoto.Day);

            _dateOfPhoto = Convert.ToDateTime(TextBoxObsDate.Text);

            // get the ABC Form Data

            abcSubmit = new ABCSubmit(locationName, FirstName, LastName, _dateOfPhoto, _bandCode, speciesName);

            _nameAndLocation = string.Format("<h3> {0} <i>({1})</i>, {2}, {3} County</h3>", speciesName, _latinName, locationName, countyName);
            _credits = string.Format("<p><i>{0}, photo by {1} {2}</i></p>", _dateOfPhoto.ToString("dd MMMM yyyy"), FirstName, LastName) + '\n';

            saveUpLoadedFilesABC();

            abcSubmit.createWriters();

            abcSubmit.writeHeader();

            //Write the Credits
            writeABCDocumentation();

            abcSubmit.writeImageLocations(_image1, _image2, _image3, _image4);

            abcSubmit.writeFooter();

            abcSubmit.writeReturnLinks();

            abcSubmit.insertDataRecord();

            abcSubmit.closeFiles();

        }

        private void writeABCDocumentation()
        {
            string doc = NewformatABCHTMLText();

            abcSubmit.writeABCDocumentation(doc);
            // throw new NotImplementedException();
        }

        //Format the Message Body
        //*******************************************************************************************************************
        public string formatABCEmailMessage()
        {
            string idedBefore = "";
            string NotesTaken = "";
            string HowWellSeen = "";
            string Photographed = "";
            string SubmittedBy = string.Empty;

            if (PhotoYes.Checked)
                Photographed = "yes";
            if (PhotoNo.Checked)
                Photographed = "No";

            if (Radio_Id_Yes.Checked)
                idedBefore = "yes";
            if (Radio_Id_No.Checked)
                idedBefore = "No";

            if (RadioNotesDuring.Checked)
                NotesTaken = "During";
            if (RadioNotesAfter.Checked)
                NotesTaken = "After";

            if (RadioVeryWell.Checked)
                HowWellSeen = "Very Well";
            if (RadioModeratelyWell.Checked)
                HowWellSeen = "Moderately Well";
            if (RadioOK.Checked)
                HowWellSeen = "OK";
            if (RadioPoorly.Checked)
                HowWellSeen = "Poorly";

            if (CheckBoxOtherSubmitter.Checked == true)
                SubmittedBy = TextAreaOtherSubmitter.Value + '\r' + '\n' + '\r' + '\n' + '\n';

            string formattedMessage = speciesName + '\r' + '\n'
                + '\r' + '\n'
                + "Date observed: " + _dateOfPhoto.ToString("dd MMMM yyyy") + '\r' + '\n'
                 + "Time observed: " + TextBoxTimeObserved.Text + '\r' + '\n'
                  + "Number observed: " + NumberObserved.Value + '\r' + '\n'
                   + "Age: " + Age.Value + '\r' + '\n'
                    + "Sex: " + Sex.Value + '\r' + '\n'
                    + '\r' + '\n'
                    + "Submitted by: " + TextBoxFirstName.Text + " " + TextBoxLastName.Text + '\r' + '\n'
                    + '\r' + '\n'
                    + SubmittedBy
                     + "Exact location: " + Location.Value + '\r' + '\n'
                      + "County: " + County.SelectedItem.Value + '\r' + '\n'
                       + "City: " + City.Value + '\r' + '\n'
                        + "Elevation: " + Elevation.Value + '\r' + '\n'
                        + '\r' + '\n'
                         + "Duration: " + Duration.Value + '\r' + '\n'
                          + "Distance: " + Distance.Value + '\r' + '\n'
                           + "Optics used: " + Optics.Value + '\r' + '\n'
                            + "Position of sun: " + Sun.Value + '\r' + '\n'
                             + "habtitat: " + Habitat.Value + '\r' + '\n'
                             + '\r' + '\n'
                              + "Initial observers: " + InitialObserver.Value + '\r' + '\n'
                               + "Others: " + OtherObservers.Value + '\r' + '\n'
                                + "Anyone known to disagree: " + Disagree.Value + '\r' + '\n'
                                 + '\r' + '\n'
                                 + "Was the bird identified before consulting any guides: " + idedBefore + '\r' + '\n'
                                 + "Identification guides used: " + GuidesUsed.Value + '\r' + '\n'
                                  + "Were notes taken before or after the observation: " + NotesTaken + '\r' + '\n'
                                  + '\n'
                                  + "Detailed description: " + '\r' + '\n' + Description.Value + '\r' + '\n'
                                  + '\r' + '\n'
                                  + "Similar species considered and why: " + '\r' + '\n' + SimilarSpecies.Value + '\r' + '\n'
                                      + '\r' + '\n'
                                       + "Experience with species: " + Experience.Value + '\r' + '\n'
                                       + '\r' + '\n'
                                        + "Experience with similar species: " + ExperienceSimilar.Value + '\r' + '\n'
                                        + '\r' + '\n'
                                   + '\r' + '\n'
                                   + "Vocalizations: " + Vocalization.Value + '\r' + '\n'
                                    + "Behavior: " + Behavior.Value + '\r' + '\n'
                                   + '\r' + '\n'
                                    + "How well was the bird seen: " + HowWellSeen + '\r' + '\n'
                                   + "Was the bird photographed: " + Photographed + '\r' + '\n'
                                   + '\r' + '\n'
                                           + "Submitter name: " + TextBoxFirstName.Text + " " + TextBoxLastName.Text + '\r' + '\n'
                                            + "Submitter email: " + TextBoxEmailAddress.Text + '\r' + '\n'
                                              + "Date and time submitted:" + DateTime.Now + '\r' + '\n';


            return (formattedMessage);
        }
        //Format the Message Body
        //*******************************************************************************************************************
        public string formatABCHTMLText()
        {
            string idedBefore = "";
            string NotesTaken = "";
            string HowWellSeen = "";
            string Photographed = "";
            string SubmittedBy = string.Empty;

            if (PhotoYes.Checked)
                Photographed = "yes";
            if (PhotoNo.Checked)
                Photographed = "No";

            if (Radio_Id_Yes.Checked)
                idedBefore = "yes";
            if (Radio_Id_No.Checked)
                idedBefore = "No";

            if (RadioNotesDuring.Checked)
                NotesTaken = "During";
            if (RadioNotesAfter.Checked)
                NotesTaken = "After";

            if (RadioVeryWell.Checked)
                HowWellSeen = "Very Well";
            if (RadioModeratelyWell.Checked)
                HowWellSeen = "Moderately Well";
            if (RadioOK.Checked)
                HowWellSeen = "OK";
            if (RadioPoorly.Checked)
                HowWellSeen = "Poorly";

            if (CheckBoxOtherSubmitter.Checked == true)
                SubmittedBy = TextAreaOtherSubmitter.Value + '\r' + '\n' + '\r' + '\n' + '\n';

            string formattedHTMLText = "<p>"
                + "<b>" + speciesName + "</b>" + " " + "<i>(" + _latinName + ")</i>"
                + "</p>"
                + "Date observed: " + _dateOfPhoto.ToString("dd MMMM yyyy") + "<br />"
                 + "Time observed: " + TextBoxTimeObserved.Text + "<br />"
                  + "Number observed: " + NumberObserved.Value + "<br />"
                   + "Age: " + Age.Value + "<br />"
                    + "Sex: " + Sex.Value + "<br />"
                    + "<br />"
                    + "Submitted by: " + TextBoxFirstName.Text + " " + TextBoxLastName.Text + "<br />"
                    + "<br />"
                    + SubmittedBy
                     + "Exact location: " + Location.Value + "<br />"
                      + "County: " + County.SelectedItem.Value + "<br />"
                       + "City: " + City.Value + '\n'
                        + "Elevation: " + Elevation.Value + "<br />"
                        + '\n'
                         + "Duration of observation: " + Duration.Value + "<br />"
                          + "Minimun distance from bird: " + Distance.Value + "<br />"
                           + "Optics used: " + Optics.Value + "<br />"
                            + "Position of sun: " + Sun.Value + "<br />"

                             + "Habtitat: " + Habitat.Value + "<br />"
                             + "<br />"
                              + "Initial observers: " + InitialObserver.Value + "<br />"
                               + "Others who observed this bird: " + OtherObservers.Value + "<br />"
                                + "Anyone known to disagree: " + Disagree.Value + "<br />"
                                + "<br />"
                                 + "Was the bird identified before consulting any guides: " + idedBefore + "<br />"
                                 + "Identification guides used: " + GuidesUsed.Value + "<br />"
                                  + "Were notes taken before or after the observation: " + NotesTaken + "<br />"
                                  + "<br />"
                                  + "Detailed description: " + '\n' + Description.Value + "<br />"
                                  + "<br />"
                                  + "Similar species considered and why: " + SimilarSpecies.Value + "<br />"
                                      + "<br />"
                                       + "Experience with species: " + Experience.Value + "<br />"
                                       + "<br />"
                                        + "Experience with similar species: " + ExperienceSimilar.Value + "<br />"
                                   + "<br />"
                                   + "Vocalizations: " + Vocalization.Value + "<br />"
                                    + "Behavior: " + Behavior.Value + "<br />"
                                   + "<br />"
                                    + "How well was the bird seen: " + HowWellSeen + "<br />"
                                   + "Was the bird photographed: " + Photographed + "<br />"
                                   + "<br />"
                                           + "Submitter name: " + TextBoxFirstName.Text + " " + TextBoxLastName.Text + "<br />"
                                            + "Submitter email: " + TextBoxEmailAddress.Text + "<br />"
                                              + "Date and time submitted:" + DateTime.Now + "<p></p>"
                                              + "*************************************************************<p></p>";

            return (formattedHTMLText);
        }

        //Format the Message Body
        //*******************************************************************************************************************
        public string NewformatABCHTMLText()
        {
            string idedBefore = "";
            string NotesTaken = "";
            string HowWellSeen = "";
            string Photographed = "";
            string SubmittedBy = string.Empty;

            if (PhotoYes.Checked)
                Photographed = "yes";
            if (PhotoNo.Checked)
                Photographed = "No";

            if (Radio_Id_Yes.Checked)
                idedBefore = "yes";
            if (Radio_Id_No.Checked)
                idedBefore = "No";

            if (RadioNotesDuring.Checked)
                NotesTaken = "During";
            if (RadioNotesAfter.Checked)
                NotesTaken = "After";

            if (RadioVeryWell.Checked)
                HowWellSeen = "Very Well";
            if (RadioModeratelyWell.Checked)
                HowWellSeen = "Moderately Well";
            if (RadioOK.Checked)
                HowWellSeen = "OK";
            if (RadioPoorly.Checked)
                HowWellSeen = "Poorly";

            if (CheckBoxOtherSubmitter.Checked == true)
                SubmittedBy = TextAreaOtherSubmitter.Value + '\r' + '\n' + '\r' + '\n' + '\n';


            string formattedHTMLText = string.Format(@"<p>                        
                        <h3><br>
						<b>{0}</b> <i>({1})</i></h3>
						<p></p>
						<b>Date observed:</b> {2}<br>
						<b>Initial observer(s):</b> {3} <a href='{4}'></a><br>
							<b>Exact location:</b> {5}<br>
							<b>County:</b> {6}<br>
							<b>City:</b> {7}<br>
						    <b>Elevation:</b> {8}</p> 
						<p><b>Submitted by:</b> {9} {10}. {37}</p>
                            
						<p><b>Number observed:</b> {11}<br />
							<b>Age:</b> {12}<br />
							<b>Sex:</b> {13}</p>
						<p><b>Time observed:</b> {14}<br />
							<b>Duration of observation:</b> {15}<br />
							<b>Minimum distance from bird:</b> {16}<br />
							<b>Optics used:</b> {17}<br />
							<b>Position of sun:</b> {18}<br />
							<b>Habtitat:</b> {19}<br />
							<br />
							<b>Others who observed this bird:</b> {20}<br />
							<b>Anyone known to disagree:</b> {21}<br />
							<br />
							<b>Was the bird identified before consulting any guides:</b> {22}<br />
							<b>Identification guides used:</b> {23}<br />
							<b>Were notes taken before or after the observation:</b> {24}<br>
							<b>How well was the bird seen:</b> {25}<br />
							<br />
							<b>Detailed description: </b><br>
							<br>{26}</p>
						<p><b>Vocalizations:</b></p>
						<p>{27}</p>
						<p><b>Behavior: </b></p>
						<p>{28}<br />
							<br />
							<b>What similar species were considered and why were they eliminated:</b></p>
						<p>{29}<br />
							<br />
							<b>Experience with species:</b></p>
						<p>{30}<br />
							<br />
							<b>Experience with similar species: </b></p>
						<p>{31}<br />
							<br />
							<b>Was the bird photographed:</b> {32}<br />
							<br />
							<b>Submitter name:</b> {33} {34}<br />
							<b>Submitter email:</b> {35}<br />
							<b>Date and time submitted:</b> {36}</p>
						<p></p>
						*************************************************************
						<p></p>
						<br>", speciesName, _latinName, _dateOfPhoto.ToString("dd MMMM yyyy"), InitialObserver.Value, TextBoxEmailAddress.Text, Location.Value, County.SelectedItem.Value, City.Value, Elevation.Value, TextBoxFirstName.Text, TextBoxLastName.Text, NumberObserved.Value, Age.Value, Sex.Value, TextBoxTimeObserved.Text, Duration.Value, Distance.Value, Optics.Value, Sun.Value, Habitat.Value, OtherObservers.Value, Disagree.Value, idedBefore, GuidesUsed.Value, NotesTaken, HowWellSeen, Description.Value, Vocalization.Value, Behavior.Value, SimilarSpecies.Value, Experience.Value, ExperienceSimilar.Value, Photographed, TextBoxFirstName.Text, TextBoxLastName.Text, TextBoxEmailAddress.Text, DateTime.Now, SubmittedBy);


            return (formattedHTMLText);
        }


        #endregion

        protected void CheckBoxOtherSubmitter_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxOtherSubmitter.Checked == true)
            {
                TextBoxOnBehalfOf.Visible = true;
                LabelOtherSubmitter.Visible = true;
                TextAreaOtherSubmitter.Visible = true;
            }
            else
            {
                TextBoxOnBehalfOf.Visible = false;
                LabelOtherSubmitter.Visible = false;
                TextAreaOtherSubmitter.Visible = false;
            }
        }
    }
}