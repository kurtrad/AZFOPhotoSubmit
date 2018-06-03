using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;

namespace AZFOPhotoSubmit
{
    public partial class AZFOSubmitMain : System.Web.UI.Page
    {
        AZFOSubmit azfoSubmit = null;
        ABCSubmit abcSubmit = null;
        string _nameAndLocation = string.Empty;
        string _image1 = string.Empty;
        string _image2 = string.Empty;
        string _image3 = string.Empty;
        string _image4 = string.Empty;

        string _SubmitterFirstName = string.Empty;
        string _SubmitterLastName = string.Empty;
        string _PhotographerFirstName = string.Empty;
        string _PhotographerLastName = string.Empty;

        string _urlReturn = @"http://www.azfo.org/gallery/photos.html";
        string speciesName = string.Empty;
        string _credits = string.Empty;
        string _bandCode = string.Empty;
        DateTime _dateOfPhoto = DateTime.Now;
        const string thankYou = @"http://www.azfo.org/gallery/photoSubmit/thankyou.html";
        const string thankYouABC = @"http://www.azfo.org/gallery/photoSubmit/thankyouABC.html";
        bool _fileSave = false;
        string _filename1FullPath = string.Empty;
        string _filename2FullPath = string.Empty;
        string _filename3FullPath = string.Empty;
        string _filename4FullPath = string.Empty;
        string _file1Name = string.Empty;
        string _file2Name = string.Empty;
        string _file3Name = string.Empty;
        string _file4Name = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string reviewStatus = string.Empty;

            //// no date selected
            //if (TextBoxObservationDate.Text == string.Empty)
            //{
            //    LabelValidDate.Visible = true;
            //    return;
            //}
            // use to test
            // string email = formatABCEmailMessage();

            if (Panel1.Visible == true)
            {
                BuildAZFOHTML();
                reviewStatus = getSpeciesReviewStatus();
                if (reviewStatus == "Y")
                {
                    Panel2.Visible = true;
                    ButtonReview.Text = "Continue";
                    ButtonExit.Visible = false;
                }
            }

            if (Panel2.Visible == true && Panel1.Visible == false)
            {
                if (RadioButtonList1.SelectedValue != "includeABCReport")
                    InitialObserver.Value = String.Format("{0} {1} submitted this record via the AZFO Photo Documentation Form, but chose not to fill out the ABC Data", TextBoxFirstName.Text, TextBoxLastName.Text);

                BuildABCHTML();
            }

            // temp stop email
            bool pigsfly = true;

            if (Panel1.Visible == true && pigsfly == true)
                azfoSubmit.SendMail(TextBoxEmailAddress.Text);

            // set AZFO Panel to false and show panel2 ABC panel
            Panel1.Visible = false;

            if (RadioButtonList1.SelectedValue == "includeABCReport" && pigsfly == true)
            {
                string formattedMessage = NewformatABCHTMLText();
                abcSubmit.SendMail(TextBoxEmailAddress.Text, formattedMessage);
            }

            // redirect to a thank you
            if (RadioButtonList1.SelectedValue == "No" || reviewStatus == "N")
                Response.Redirect("thankYou.html");

            // redirect to a thank you
            if (RadioButtonList1.SelectedValue == "includeABCReport")
                Response.Redirect("thankYouABC.html");
        }
        #region build AZFO Html

        private void BuildAZFOHTML()
        {
            string latinName = DropDownListOfSpecies.SelectedItem.Value;
            string countyName = DropDownListCounty.SelectedItem.Text;

            if (CheckBoxNotPhotographer.Checked == true)
            {
                _PhotographerFirstName = TextboxPhotographersFName.Text;
                _PhotographerLastName = TextBoxPhotographerLName.Text.Replace(' ', '_');
                _SubmitterFirstName = TextBoxFirstName.Text;
                _SubmitterLastName = TextBoxLastName.Text.Replace(' ', '_'); 
            }
            else
            {
                _PhotographerFirstName = TextBoxFirstName.Text;
                _PhotographerLastName = TextBoxLastName.Text;
                _SubmitterFirstName = TextBoxFirstName.Text;
                _SubmitterLastName = TextBoxLastName.Text.Replace(' ', '_'); 
            }

            //string FirstName = TextBoxFirstName.Text;
            string emailAddress = TextBoxEmailAddress.Text;

            string locationName = filterForFileName(TextBoxShortLocation.Text);
            string locationLongName = TextBoxLocationName.Text;
            //string LastName = TextBoxLastName.Text.Replace(' ', '_');
            //string date = string.Format("{0}{1}{2}", _dateOfPhoto.Year, _dateOfPhoto.Month, _dateOfPhoto.Day);

            _dateOfPhoto = Convert.ToDateTime(TextBoxObservationDate.Text.Trim());
            string date = _dateOfPhoto.ToString("dd_MMMM_yyyy");

            //LabelValidDate.Visible = false;

            if (TextBoxOther.Text == string.Empty)
            {
                speciesName = DropDownListOfSpecies.SelectedItem.Text;
                _bandCode = getSpeciesBandCode();
            }
            else
            {
                speciesName = TextBoxOther.Text.Replace("'", "_");
                latinName = "Latin Name";
                _bandCode = speciesName.Replace(' ', '_');
            }

            azfoSubmit = new AZFOSubmit(locationName, _SubmitterFirstName, _SubmitterLastName, date, _bandCode, speciesName);

            _nameAndLocation = string.Format("<h3> {0} <i>({1})</i>, {2}, {3} County</h3>", speciesName, latinName, locationLongName, countyName);
            _credits = string.Format("<p><i>{0}, photo by {1} {2}</i></p>", _dateOfPhoto.ToString("dd MMMM yyyy"), _PhotographerFirstName, _PhotographerLastName) + '\n';

            saveUpLoadedFilesAZFO();

            azfoSubmit.createWriters();

            azfoSubmit.writeHeader();

            //Write the Credits
            writeCredits();

            azfoSubmit.writeFooter();

            azfoSubmit.writeReturnLinks();

            azfoSubmit.closeFiles();

        }

        private void writeName()
        {
            string SubmitInfo = "SubmitInfo";

            SubmitInfo = string.Format("<p>This {0} was photographed by {1} {2} on {3}<br></p>", speciesName, _PhotographerFirstName, _PhotographerLastName, _dateOfPhoto.ToString("dd MMMM yyyy"));

            azfoSubmit.writeName(SubmitInfo);
        }

        private void writeIDPoints()
        {
            string IDPoints = "ID Points:";

            if (TextareaIDPoints.Value == string.Empty)
                IDPoints = string.Format("<p>{0}<br></p>", IDPoints);
            else
                IDPoints = string.Format("<p>{0}<br></p>", TextareaIDPoints.Value);

            azfoSubmit.writeIDPoints(IDPoints);
        }

        private void writeStatus()
        {
            string status = "Status and Distribution Information:";

            if (TextareaStatus.Value == string.Empty)
                status = string.Format("<p>{0}<br></p>", status);
            else
                status = string.Format("<p>{0}<br></p>", TextareaStatus.Value);

            azfoSubmit.writeStatus(status);
        }

        private void writeNotes()
        {
            string notes = "General notes to the photo editor including why this species is being submitted to AZFO:";

            if (TextAreaNotes.Value == string.Empty)
                notes = string.Format("<p>{0}<br></p>", notes);
            else
                notes = string.Format("<p>{0}<br></p>", TextAreaNotes.Value);

            azfoSubmit.writeNotes(notes);
        }

        #endregion

        #region build ABC Html

        private void BuildABCHTML()
        {
            string latinName = DropDownListOfSpecies.SelectedItem.Value;
            string countyName = DropDownListCounty.SelectedItem.Text;

            if (CheckBoxNotPhotographer.Checked == true)
            {
                _PhotographerFirstName = TextboxPhotographersFName.Text;
                _PhotographerLastName = TextBoxPhotographerLName.Text.Replace(' ', '_'); ;
            }
            else
            {
                _PhotographerFirstName = TextBoxFirstName.Text;
                _PhotographerLastName = TextBoxLastName.Text;
                _SubmitterFirstName = TextBoxFirstName.Text;
                _SubmitterLastName = TextBoxLastName.Text.Replace(' ', '_'); ;
            }

            //string FirstName = TextBoxFirstName.Text;
            string emailAddress = TextBoxEmailAddress.Text;

            string locationName = filterForFileName(TextBoxShortLocation.Text);
            //string LastName = TextBoxLastName.Text.Replace(' ', '_');
            //string date = string.Format("{0}{1}{2}", _dateOfPhoto.Year, _dateOfPhoto.Month, _dateOfPhoto.Day);

            _dateOfPhoto = Convert.ToDateTime(TextBoxObservationDate.Text.Trim());
            string date = _dateOfPhoto.ToString("dd_MMMM_yyyy");

            //LabelValidDate.Visible = false;

            if (TextBoxOther.Text == string.Empty)
            {
                speciesName = DropDownListOfSpecies.SelectedItem.Text;
                _bandCode = getSpeciesBandCode();
            }
            else
            {
                speciesName = TextBoxOther.Text;
                latinName = "Latin Name";
                _bandCode = speciesName.Replace(' ', '_');
            }

            // get the ABC Form Data

            abcSubmit = new ABCSubmit(locationName, _SubmitterFirstName, _SubmitterLastName, _dateOfPhoto, _bandCode, speciesName);

            _nameAndLocation = string.Format("<h3> {0} <i>({1})</i>, {2}, {3} County</h3>", speciesName, latinName, locationName, countyName);
            _credits = string.Format("<p><i>{0}, photo by {1} {2}</i></p>", _dateOfPhoto.ToString("dd MMMM yyyy"), _PhotographerFirstName, _PhotographerLastName) + '\n';

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
        public string NewformatABCHTMLText()
        {
            string idedBefore = "";
            string NotesTaken = "";
            string HowWellSeen = "";
            string Photographed = "";
            string SubmittedBy = string.Empty;

            Photographed = "yes";

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

            String LocationPhotographed = Location.Value;
            if (RadioButtonList1.SelectedValue != "includeABCReport")
                LocationPhotographed = TextBoxLocationName.Text;

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
						<p><b>Submitted by:</b> {9} {10}</p>
                            
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
						<br>", speciesName, DropDownListOfSpecies.SelectedItem.Value, _dateOfPhoto.ToString("dd MMMM yyyy"), InitialObserver.Value, TextBoxEmailAddress.Text, LocationPhotographed, DropDownListCounty.SelectedItem.Value, TextBoxCity.Text, Elevation.Value, TextBoxFirstName.Text, TextBoxLastName.Text, NumberObserved.Value, Age.Value, Sex.Value, TextBoxTimeObserved.Text, Duration.Value, Distance.Value, Optics.Value, Sun.Value, Habitat.Value, OtherObservers.Value, Disagree.Value, idedBefore, GuidesUsed.Value, NotesTaken, HowWellSeen, Description.Value, Vocalization.Value, Behavior.Value, SimilarSpecies.Value, Experience.Value, ExperienceSimilar.Value, Photographed, TextBoxFirstName.Text, TextBoxLastName.Text, TextBoxEmailAddress.Text, DateTime.Now);


            return (formattedHTMLText);
        }


        //Format the Message Body
        //*******************************************************************************************************************
        public string formatABCEmailMessage()
        {
            string idedBefore = "";
            string NotesTaken = "";
            string HowWellSeen = "";
            string Photographed = "";

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

            string formattedMessage = DropDownListOfSpecies.SelectedItem.Text + '\r' + '\n'
                + '\r' + '\n'
                + "Date observed: " + _dateOfPhoto.ToString("dd MMMM yyyy") + '\r' + '\n'
                 + "Time observed: " + TextBoxTimeObserved.Text + '\r' + '\n'
                  + "Number observed: " + NumberObserved.Value + '\r' + '\n'
                   + "Age: " + Age.Value + '\r' + '\n'
                    + "Sex: " + Sex.Value + '\r' + '\n'
                    + '\r' + '\n'
                    + "Submitted by: " + TextBoxFirstName.Text + " " + TextBoxLastName.Text + '\r' + '\n'
                    + '\r' + '\n'
                     + "Exact location: " + Location.Value + '\r' + '\n'
                      + "County: " + DropDownListCounty.SelectedItem.Value + '\r' + '\n'
                       + "City: " + TextBoxCity.Text + '\r' + '\n'
                       + '\r' + '\n'
                        + "Elevation: " + Elevation.Value + '\r' + '\n'
                        + '\r' + '\n'
                         + "Duration: " + Duration.Value + '\r' + '\n'
                          + "Distance: " + Distance.Value + '\r' + '\n'
                           + "Optics used: " + Optics.Value + '\r' + '\n'
                            + "Position of sun: " + Sun.Value + '\r' + '\n'
                             + "habitat: " + Habitat.Value + '\r' + '\n'
                             + '\r' + '\n'
                              + "Initial observers: " + InitialObserver.Value + '\r' + '\n'
                               + "Others: " + OtherObservers.Value + '\r' + '\n'
                                + "Anyone known to disagree: " + Disagree.Value + '\r' + '\n'
                                 + '\r' + '\n'
                                 + "Was the bird identified before consulting any guides: " + idedBefore + '\r' + '\n'
                                 + "Identification guides used: " + GuidesUsed.Value + '\r' + '\n'
                                  + "Were notes taken during or after the observation: " + NotesTaken + '\r' + '\n'
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

            string formattedHTMLText = "<p>"
                + "<b>" + DropDownListOfSpecies.SelectedItem.Text + "</b>" + " " + "<i>(" + DropDownListOfSpecies.SelectedItem.Value + ")</i>"
                + "</p>"
                + "Date observed: " + _dateOfPhoto.ToString("dd MMMM yyyy") + "<br />"
                 + "Time observed: " + TextBoxTimeObserved.Text + "<br />"
                  + "Number observed: " + NumberObserved.Value + "<br />"
                   + "Age: " + Age.Value + "<br />"
                    + "Sex: " + Sex.Value + "<br />"
                    + "<br />"
                    + "Submitted by: " + TextBoxFirstName.Text + " " + TextBoxLastName.Text + "<br />"
                    + "<br />"
                     + "Exact location: " + Location.Value + "<br />"
                      + "County: " + DropDownListCounty.SelectedItem.Value + "<br />"
                       + "City: " + TextBoxCity.Text + "<br />"
                        + "Elevation: " + Elevation.Value + "<br />"
                        + '\n'
                         + "Duration of observation: " + Duration.Value + "<br />"
                          + "Minimum distance from bird: " + Distance.Value + "<br />"
                           + "Optics used: " + Optics.Value + "<br />"
                            + "Position of sun: " + Sun.Value + "<br />"

                             + "Habitat: " + Habitat.Value + "<br />"
                             + "<br />"
                              + "Initial observers: " + InitialObserver.Value + "<br />"
                               + "Others who observed this bird: " + OtherObservers.Value + "<br />"
                                + "Anyone known to disagree: " + Disagree.Value + "<br />"
                                + "<br />"
                                 + "Was the bird identified before consulting any guides: " + idedBefore + "<br />"
                                 + "Identification guides used: " + GuidesUsed.Value + "<br />"
                                  + "Were notes taken during or after the observation: " + NotesTaken + "<br />"
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


        private void writeCredits()
        {
            azfoSubmit.writeNameLocation(_nameAndLocation);

            //Insert the Notes and Info
            writeName();
            writeNotes();
            writeStatus();
            writeIDPoints();

            // azfoSubmit.writeABCInfo();
            azfoSubmit.writeImageLocations(_image1, _image2, _image3, _image4);
            azfoSubmit.writeCredit(_credits);
        }

        #endregion

        #region Private Methods
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

        private string getSpeciesReviewStatus()
        {
            string reviewStatus = "review";
            XmlDocument doc = new XmlDocument();
            doc = XmlDataSourceReviewSpecies.GetXmlDocument();

            XmlNode node;
            XmlNode review;
            XmlNode root = doc.DocumentElement;
            string xmlspeciesname = string.Empty;

            xmlspeciesname = DropDownListOfSpecies.SelectedItem.Text;

            if (xmlspeciesname.Contains("'"))
            {
                xmlspeciesname = buildXPathString(xmlspeciesname);
                node = root.SelectSingleNode(string.Format("Species[@EnglishName ={0}]", xmlspeciesname));
            }
            else
            {
                node = root.SelectSingleNode(string.Format("Species[@EnglishName ='{0}']", xmlspeciesname));
            }

            review = node.Attributes.GetNamedItem("Review");
            //Get the Review statust
            reviewStatus = review.InnerText;
            return reviewStatus;
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
            //Console.WriteLine(result);
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
            fileNameNew = fileNameNew.Replace(" ", "");
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

        #endregion

        #region save AZFO Files
        private void saveUpLoadedFilesAZFO()
        {
            _fileSave = false;
            bool fileSave1 = true;
            bool fileSave2 = true;
            bool fileSave3 = true;
            bool fileSave4 = true;
            LabelFileType1.Visible = false;
            LabelFileType2.Visible = false;
            LabelFileType3.Visible = false;
            LabelFileType4.Visible = false;

            string file1 = azfoSubmit.getFileName;
            char delimiterChars = '.';

            if (FileUpload1.FileName != string.Empty)
            {
                LabelFileType1.Visible = false;

                if (validateFileExtension(FileUpload1))
                {
                    // what kind of file is this
                    //FileInfo f = new FileInfo(FileUpload1.FileName);
                    //string extension = f.Extension;
                    string extension = Path.GetExtension(FileUpload1.FileName).ToLower();
                    
                    _file1Name = string.Format("{0}_a{1}", file1, extension);
                    _filename1FullPath = string.Format("{0}{1}_a{2}", azfoSubmit.getImagePath, file1, extension);

                    FileUpload1.SaveAs(_filename1FullPath);
                    //_image1 = string.Format("<p><img src='../submittedImages/{0}' width='600' height='464' border='0' align='bottom'></p>", file1Name);
                    _image1 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='750'></p>", _file1Name);

                }
                else
                {
                    LabelFileType1.Text = "Only image files are accepted";
                    LabelFileType1.Visible = true;
                    fileSave1 = false;
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
                    _filename2FullPath = string.Format("{0}{1}_b{2}", azfoSubmit.getImagePath, file1, extension);

                    FileUpload2.SaveAs(_filename2FullPath);
                    _image2 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='750'></p>", file2Name);

                }
                else
                {
                    LabelFileType2.Text = "Only image files are accepted";
                    LabelFileType2.Visible = true;
                    fileSave2 = false;
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
                    _filename3FullPath = string.Format("{0}{1}_c{2}", azfoSubmit.getImagePath, file1, extension);

                    FileUpload3.SaveAs(_filename3FullPath);
                    _image3 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='750'></p>", file3Name);

                }
                else
                {
                    LabelFileType3.Text = "Only image files are accepted";
                    LabelFileType3.Visible = true;
                    fileSave3 = false;
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
                    _filename4FullPath = string.Format("{0}{1}_d{2}", azfoSubmit.getImagePath, file1, extension);

                    FileUpload4.SaveAs(_filename4FullPath);
                    _image4 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='750'></p>", file4Name);

                }
                else
                {
                    LabelFileType4.Text = "Only image files are accepted";
                    LabelFileType4.Visible = true;
                    fileSave4 = false;
                }
            }

            if (fileSave1 == true && fileSave2 == true && fileSave3 == true && fileSave4 == true)
                _fileSave = true;

        }

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
        #endregion
        #region save ABC Files
        private void saveUpLoadedFilesABC()
        {
            string locationName = filterForFileName(TextBoxShortLocation.Text);
            string FirstName = TextBoxFirstName.Text;
            string LastName = TextBoxLastName.Text.Replace(' ', '_');
            string date = _dateOfPhoto.ToString("dd_MMMM_yyyy");
            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp" };
            String[] fileIncrement = { "a", "b", "c", "d" };
            char delimiterChars = '.';

            azfoSubmit = new AZFOSubmit(locationName, FirstName, LastName, date, _bandCode, speciesName);

            string fromfile1 = azfoSubmit.getFileName;

            foreach (string increment in fileIncrement)
            {
                foreach (string ext in allowedExtensions)
                {
                    string fromfilename1FullPath = string.Format("{0}{1}_{2}{3}", azfoSubmit.getImagePath, fromfile1, increment, ext);

                    FileInfo f = new FileInfo(fromfilename1FullPath);
                    if (f.Exists)
                    {
                        string tofilename1FullPath = string.Format("{0}{1}", abcSubmit.getImagePath, f.Name);
                        File.Copy(fromfilename1FullPath, tofilename1FullPath, true);
                        if (increment == "a")
                            _image1 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='850'></p>", f.Name);
                        if (increment == "b")
                            _image2 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='850'></p>", f.Name);
                        if (increment == "c")
                            _image3 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='850'></p>", f.Name);
                        if (increment == "d")
                            _image4 = string.Format("<p><img src='../submittedImages/{0}' border='0' align='bottom' width ='850'></p>", f.Name);
                    }
                }
            }
        }

        #endregion

        protected void ButtonExit_Click(object sender, EventArgs e)
        {
            Response.Redirect(_urlReturn);
        }


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "includeABCReport")
            {
                TABLE1.Visible = true;
                ButtonReview.Visible = true;

            }
            else
            {
                TABLE1.Visible = false;

            }
        }
        protected void DropDownListOfSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            string reviewStatus = getSpeciesReviewStatus();
            string xmlspeciesname = DropDownListOfSpecies.SelectedItem.Text;

            if (DropDownListOfSpecies.SelectedIndex == 1)
            {
                TextBoxOther.Visible = true;
                LabelOther.Visible = true;
            }
            else
            {
                TextBoxOther.Visible = false;
                LabelOther.Visible = false;

            }

            RadioButtonList1.SelectedIndex = -1;

            if (reviewStatus == "Y")
            {
                // Panel1.Visible = false;
                RadioButtonList1.Visible = true;
                LabelReviewSpecies.Text = string.Format("Thank you for submitting photos to AZFO and Congratulations!! {0} is a Review Species, please consider providing the few extra details necessary to make your sighting an official report to the Arizona Bird Committee. Reports are welcome even if you are not the original observer. Select an option below", xmlspeciesname);
                LabelReviewSpecies.Visible = true;
                LabelReviewSpecies.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                TABLE1.Visible = false;
                RadioButtonList1.Visible = false;
                LabelReviewSpecies.Visible = false;
            }

        }

        protected void CheckBoxNotPhotographer_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxNotPhotographer.Checked == true)
                PanelPhotographer.Visible = true;
            else
                PanelPhotographer.Visible = false;


        }
    }
}
