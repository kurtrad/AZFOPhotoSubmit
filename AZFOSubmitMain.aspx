<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/AZFOPhotoSubmit.Master" CodeBehind="AZFOSubmitMain.aspx.cs" Inherits="AZFOPhotoSubmit.AZFOSubmitMain" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: x-small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <form id="form1" runat="server">   
    <table width='700' border='0' cellspacing='0' cellpadding='0' align='center'>
			
			<tr>
				<td align='left' class='background' valign='top' width='700'>
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                </td>
			</tr>
			<tr height='10'>
				<td valign='top' width='700'>
				
				<asp:Panel ID="Panel1" runat="server">
                </td>
			</tr>
			<tr>
				<td align='left' valign='top' width='700'>
					<div>
        <h2>
            Submit Photo Documentation to AZFO</h2>
        <h4>
            denotes required field <span style="color: #cc0000">*</span>
        </h4>  
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
		<p>
            Please familiarize yourself with the Guidelines before submitting photos<br>
			(use Back button on your browser to return to Guidelines Page).</p>
    <p>
        &nbsp;Hover over information icon &nbsp;Hover over information icon
        <img alt="Hover over these icons for help" src="resources/information.png" 
            id="200" />
        for hints on specific items. </p>
                            Please select a Review or Sketch Details Species from drop down list:<br />
                        <asp:DropDownList ID="DropDownListOfSpecies" runat="server" DataSourceID="XmlDataSource1"
            Width="350px" DataTextField="EnglishName" DataValueField="LatinName" 
            AppendDataBoundItems="True" 
            ToolTip="Complete list of ABC review and sketch details species" 
            AutoPostBack="True" 
            onselectedindexchanged="DropDownListOfSpecies_SelectedIndexChanged">
        </asp:DropDownList>&nbsp;<asp:CompareValidator ID="CompareValidator1" 
            runat="server" ControlToValidate="DropDownListOfSpecies" 
            ErrorMessage="Please select a species or use other" Operator="NotEqual" 
            ValueToCompare="Selectus"></asp:CompareValidator>
        <br />
        <br />
        <asp:Label ID="LabelOther" runat="server" 
            Text="Other: Type Species Name here " Visible="False"></asp:Label>
        <br />
                        <asp:TextBox ID="TextBoxOther" runat="server" Width="340px" 
            ToolTip="Use this list if you would like to submit regional interest species, hybrids or exotics" 
            Visible="False"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                            ControlToValidate="TextBoxOther" ErrorMessage="Please enter a species name"></asp:RequiredFieldValidator>
                        <br />
                        Where did you photograph the bird?&nbsp; (Examples:&nbsp; Upper Madera Canyon, Lake Havasu 
                        or Boyce Thompson Arboretum)<br />
                        <asp:TextBox ID="TextBoxLocationName" runat="server" Width="340px" ToolTip="Detailed description of location"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxLocationName"
            ErrorMessage="Please enter a location"></asp:RequiredFieldValidator><br />
                        <asp:Image ID="ImageShortDescInfo" runat="server" 
                            ImageUrl="resources/information.png" 
                            ToolTip="Short desc is used to generate file names. e.g Gilbert Water Ranch is abbreviated to GWR. This is abitrary and made up." />
                        &nbsp;Short description of location for file naming purposes, limit 10 characters.<br>
		                (Examples:&nbsp; Madera, Havasu or BTA) <span
            style="color: #cc0000">*</span><br /><asp:TextBox ID="TextBoxShortLocation" 
                            runat="server" MaxLength="10" ToolTip="Abbreviation of the location."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="TextBoxShortLocation" 
                            ErrorMessage="Please enter a short description"></asp:RequiredFieldValidator>
                        <br />
                        City (Optional):<br />&nbsp;<asp:TextBox ID="TextBoxCity" runat="server"></asp:TextBox>
                        <br />
                        County: <span style="color: #cc0000">*</span><br />
                        <asp:DropDownList ID="DropDownListCounty" runat="server" DataSourceID="XmlDataSourceCounty"
            DataTextField="name" DataValueField="name" Width="150px" ToolTip="All 15 Arizona Counties">
        </asp:DropDownList>&nbsp;<br />
        <br />
                        Date you photographed this species (type date in MM/DD/YYYY format)
                        <br />
                        or click calender to select date: <span style="color: #cc0000">*</span><asp:MaskedEditValidator 
                            ID="MaskedEditValidator1" runat="server" 
                            ControlExtender="TextBoxObservationDate_MaskedEditExtender" 
                            ControlToValidate="TextBoxObservationDate" 
                            EmptyValueMessage="Please enter a date" 
                            ErrorMessage="Please enter a valid date (mm/dd/yyyy) format" 
                            InvalidValueMessage="Please enter a valid date (mm/dd/yyyy) format" 
                            IsValidEmpty="False"></asp:MaskedEditValidator>
                        <br />
                        <asp:TextBox ID="TextBoxObservationDate" runat="server"></asp:TextBox>
                        <asp:MaskedEditExtender ID="TextBoxObservationDate_MaskedEditExtender" 
                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                            Mask="99/99/9999" MaskType="Date" TargetControlID="TextBoxObservationDate">
                        </asp:MaskedEditExtender>
                        <asp:CalendarExtender ID="TextBoxObservationDate_CalendarExtender" 
                            runat="server" Enabled="True" PopupButtonID="DatePicker" 
                            TargetControlID="TextBoxObservationDate">
                        </asp:CalendarExtender>
                        <img ID="DatePicker" alt="Date Picker" src="images/calendar.gif" 
                            style="width: 23px; height: 23px" />
                        <span class="style1">Click calendar icon to prompt for date</span>
                        <br />
                        <br />
                        <br />
                        <asp:Image ID="ImageGeneralNotesInfo" runat="server" 
                            ImageUrl="resources/information.png" 
                            ToolTip="GENERAL NOTES: explain why the photo is noteworthy or the sighting important, especially for birds that are NOT Review or Sketch Detail species" />
                        &nbsp;General notes to the photo editor including why this species is being submitted 
                        to AZFO:<br />
                        <textarea ID="TextAreaNotes" runat="server" style="width: 514px; height: 50px"></textarea><br />
                        <br />
                        <asp:Image ID="ImageStatusNotesInfo" runat="server" 
                            ImageUrl="resources/information.png" 
                            ToolTip="In the optional &quot;Status and Distribution&quot; box of the online form, it is useful if you can write up a short description of the status and distribution of the rarity. We will probably embellish or edit what you provide. If you do not want to do this, the editors will provide it.    " />
                        &nbsp;Notes on Status and Distribution (Optional):<br />
                        <textarea ID="TextareaStatus" runat="server" style="width: 514px; height: 50px"></textarea><br />
                        <asp:Image ID="ImageIDNotesInfo" runat="server" 
                            ImageUrl="resources/information.png" 
                            ToolTip="ID POINTS: In the optional section, it is helpful if you can summarize the relevant field marks that are visible in the photograph that distinguish the rarity from similar species.  Again, we will probably embellish or edit what you provide.  Again, If you do not want to do this, the editors will provide it.  " />
                        &nbsp;&nbsp;ID points (Optional):<br />
                        <textarea ID="TextareaIDPoints" runat="server" 
                            style="width: 514px; height: 50px"></textarea><br />
                        <br />
                        <asp:CheckBox ID="CheckBoxNotPhotographer" runat="server" AutoPostBack="True" 
                            oncheckedchanged="CheckBoxNotPhotographer_CheckedChanged" 
                            Text="Please check here if you are submitting photos on someone else's behalf" />
                        <br />
                        <asp:Panel ID="PanelPhotographer" runat="server" Visible="False">
                            <br />
                            <asp:Label ID="LabelPhotographerFName" runat="server" 
                                Text="Photographer's First Name"></asp:Label>
                            : <span style="color: #cc0000">*</span><br />
                            <asp:TextBox ID="TextboxPhotographersFName" runat="server"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                ControlToValidate="TextboxPhotographersFName" 
                                ErrorMessage="Please enter photographers first name"></asp:RequiredFieldValidator>
                            <br />
                            <asp:Label ID="LabelPhotographerLName" runat="server" 
                                Text="Photographer's Last Name"></asp:Label>
                            : <span style="color: #cc0000">*</span><br />
                            <asp:TextBox ID="TextBoxPhotographerLName" runat="server"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                ControlToValidate="TextBoxPhotographerLName" 
                                ErrorMessage="Please enter photographers last name"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <br />
                        Your First Name: <span style="color: #cc0000">*</span><br />
                        <asp:TextBox ID="TextBoxFirstName" runat="server" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="TextBoxFirstName" 
                            ErrorMessage="Please enter your first name"></asp:RequiredFieldValidator>
                        <br />
                        Your Last Name: <span style="color: #cc0000">*</span><br />
                        <asp:TextBox ID="TextBoxLastName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="TextBoxLastName" ErrorMessage="Please enter your last name"></asp:RequiredFieldValidator>
                        <br />
                        Your Email Address: <span style="color: #cc0000">*</span><br />
                        <asp:TextBox ID="TextBoxEmailAddress" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="TextBoxEmailAddress" ErrorMessage="email address required"></asp:RequiredFieldValidator>
                        &nbsp;<asp:RegularExpressionValidator 
                            ID="revEmailAddress" runat="server" 
                            ControlToValidate="TextBoxEmailAddress" 
                            ErrorMessage="enter a valid email address" 
                            
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <br />
                        <br />
                        Select at least one image file to upload: <span style="color: #cc0000">*</span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="FileUpload1"
            ErrorMessage="a photo is required" Width="293px"></asp:RequiredFieldValidator><br />
                        &nbsp;&nbsp;&nbsp; You may submit up to four images.<br>
		                &nbsp;&nbsp;&nbsp; Please submit ALL of your images at once (you are limited to 4) in the boxes provided below.<br /> 
		                &nbsp;&nbsp;&nbsp; Please do not submit images one at a time. If you use the AZFO form multiple times to submit <br /> 
		                &nbsp;&nbsp;&nbsp; different photos of the same bird taken on the same day, some of your files may be overwritten.<br />
                        <br /> &nbsp;&nbsp;&nbsp; Recommended file size: No larger than 300 kb, 1000x800 pixels, jpeg format<br />
    	                &nbsp;&nbsp;&nbsp; Absolute file size limit:&nbsp; 4 Mb per image<br>
		                &nbsp;&nbsp;&nbsp; Only images types of .gif, .jpeg, .jpg, .bmp and .png are allowed.&nbsp;<br>
		<br />
        <asp:FileUpload ID="FileUpload1" runat="server" Width="312px" />
    <asp:Label ID="LabelFileType1" runat="server" ForeColor="Red" Text="Label" Visible="False"
        Width="224px"></asp:Label>
                        <br>
                        <asp:FileUpload ID="FileUpload2" runat="server" Width="312px" />
                        <asp:Label ID="LabelFileType2" runat="server" ForeColor="Red" Text="Label" 
                            Visible="False" Width="224px"></asp:Label>
                        <br>
                        <asp:FileUpload ID="FileUpload3" runat="server" Width="312px" />
                        <asp:Label ID="LabelFileType3" runat="server" ForeColor="Red" Text="Label" 
                            Visible="False" Width="224px"></asp:Label>
                        <br />
                        <asp:FileUpload ID="FileUpload4" runat="server" Width="312px" />
                        <asp:Label ID="LabelFileType4" runat="server" ForeColor="Red" Text="Label" 
                            Visible="False" Width="224px"></asp:Label>
                        <br />
                    </asp:Panel>
         
         
                    <br />
                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                        <table style="width:100%;">
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="LabelReviewSpecies" runat="server" 
                                            
                                            Text="The species you are submitting photos of is a Review Species, please consider providing the few extra details necessary to make your sighting an official report to the Arizona Bird Committee. Reports are welcome even if you are not the original observer. " 
                                            Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                                            Visible="False">
                                        <asp:ListItem Value="No">No: I only wish to submit my pictures to AZFO, I do not 
                                        wish to file an official ABC report at this time</asp:ListItem>
                                        <asp:ListItem Value="includeABCReport">Yes: I'd like to submit a report to the 
                                        ABC</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                            ControlToValidate="RadioButtonList1" 
                                            ErrorMessage="Please select an option type"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    
                    <br />
         
         
        <table id="TABLE1" runat="server" border="0" cellpadding="2" cellspacing="2" visible="False"
            width="700">
            <tr>
                <td id="Td2" runat="server" style="font-weight: bold; color: green; " 
                    class="style3">
                    Arizona Bird Committee Review Species Additional Information. Please fill out as 
                    much as you can. If you are not sure about something leave the field blank.&nbsp; You 
                    do not have to complete every item.</td>
            </tr>
            <tr>
                <td id="Td66" runat="server">
                                        Initial time of observation:&nbsp;
                    <asp:TextBox ID="TextBoxTimeObserved" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td id="Td17" runat="server">
                    Duration of observation: <input id="Duration" runat="server" name="12. Duration of observation"
                        size="21" type="text" /></td>
            </tr>
            <tr>
                <td id="TD28" runat="server" style="height: 30px">
                    How many birds observed: &nbsp;<input id="NumberObserved" runat="server" name="05. Number of individuals" size="3"
                        type="text" value="1" /> Age:&nbsp;
                    <input id="Age" runat="server" name="06. Age" size="24" type="text" value="adult, juvenal, etc." /><strong>
                        </strong>
                        Sex:&nbsp;
                    <input id="Sex" runat="server" name="07. Sex" style="width: 84px" type="text" value="unknown" /></td>
            </tr>
            <tr>
                <td id="TD55" runat="server" style="height: 6px">
                    Exact location:</td>
            </tr>
            <tr>
                <td id="TD27" runat="server">
                    <textarea id="Location" runat="server" name="08. Exact location" rows="3" style="width: 575px">Exact address or specific details, such as distance and direction from nearest landmark</textarea></td>
            </tr>
            <tr>
                <td id="TD34" runat="server">
                    Elevation:
                    <input id="Elevation" runat="server" name="11. Elevation" style="width: 98px" type="text" /></td>
            </tr>
            <tr>
                <td id="TD59" runat="server" style="height: 24px">
                    Distance from bird:
                        <input id="Distance" runat="server" name="13. Distance from bird" size="18" type="text"
                            value="paced, estimated, other" />
                        Optical equipment:<input id="Optics" runat="server" name="14. Optical equipment"
                            style="width: 171px" type="text" />
                    </b>
                </td>
            </tr>
            <tr>
                <td id="TD52" runat="server">
                    Relationship of sun/observer/bird: <input id="Sun" runat="server" name="15. Relationship of sun/observer/bird"
                        style="width: 351px" type="text" /></td>
            </tr>
            <tr>
                <td id="TD44" runat="server">
                    Habitat: <input id="Habitat" runat="server" name="16. Habitat " style="width: 520px"
                        type="text" value="desert scrub, oakwoodland, etc." /></td>
            </tr>
            <tr>
                <td id="TD6" runat="server">
                    Initial Observer(s) if someone other than person filing this report:</td>
            </tr>
            <tr>
                <td id="TD40" runat="server" style="height: 59px">
                    <textarea id="InitialObserver" runat="server" name="17. Others who saw bird" style="width: 575px;
                        height: 50px">Please enter name and contact information (if known)
</textarea></td>
            </tr>
            <tr>
                <td id="TD20" runat="server">
                    Others who have independently ID'd bird:</td>
            </tr>
            <tr>
                <td id="TD5" runat="server">
                    <textarea id="OtherObservers" runat="server" name="18. Others who independently id'd bird"
                        style="width: 575px; height: 50px">Please enter name and contact information (if known)</textarea></td>
            </tr>
            <tr>
                <td id="TD42" runat="server">
                    Anybody known to disagree with ID: (Optional)</td>
            </tr>
            <tr>
                <td id="TD9" runat="server" style="height: 72px">
                    <textarea id="Disagree" runat="server" name="19. Anybody known to disagree?" style="width: 575px;
                        height: 50px">Please enter name and contact information (if known)</textarea></td>
            </tr>
            <tr>
                <td id="TD19" runat="server">
                    Describe in detail the features noted.&nbsp; <br />Size, shape, plumage characters, 
                    eye color, legs, bill, and any other unique features: </td>
            </tr>
            <tr>
                <td id="TD8" runat="server">
                    <textarea id="Description" runat="server" name="20. Description" style="width: 575px;
                        height: 100px"></textarea><asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator10" runat="server" ControlToValidate="Description" 
                        ErrorMessage="A description is required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td id="TD26" runat="server">
                    Vocalization:</td>
            </tr>
            <tr>
                <td id="TD33" runat="server">
                    <input id="Vocalization" runat="server" name="21. Vocalizations" style="width: 575px"
                        type="text" /></td>
            </tr>
            <tr>
                <td id="TD41" runat="server">
                    Behavior:</td>
            </tr>
            <tr>
                <td id="Td12" runat="server" style="height: 72px">
                    <textarea id="Behavior" runat="server" name="22. Behavior" style="width: 575px; height: 50px"></textarea></td>
            </tr>
            <tr>
                <td id="TD16" runat="server">
                    What similar species were considered and why were they eliminated:</td>
            </tr>
            <tr>
                <td id="TD36" runat="server">
                    <textarea id="SimilarSpecies" runat="server" name="23. Species eliminated and why"
                        style="width: 575px; height: 50px"></textarea></td>
            </tr>
            <tr>
                <td id="TD54" runat="server">
                    Explain previous experience with this species: 
                </td>
            </tr>
            <tr>
                <td id="TD49" runat="server">
                    <textarea id="Experience" runat="server" name="24. Previous experience with this species"
                        style="width: 575px; height: 50px"></textarea><asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator11" runat="server" 
                        ErrorMessage="Previous experience with species is required" 
                        ControlToValidate="Experience"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td id="Td13" runat="server">
                    Explain previous experience with similar species:
                </td>
            </tr>
            <tr>
                <td id="TD58" runat="server">
                    <textarea id="ExperienceSimilar" runat="server" name="25. Previous experience with similar species"
                        style="width: 575px; height: 50px"></textarea></td>
            </tr>
            <tr>
                <td id="TD50" runat="server">
                    Did you identify this bird before consulting any field guides:<input id="Radio_Id_Yes"
                        runat="server" name="26. Id'd before consulting field guides" type="radio" value="Yes" />Yes
                    <input id="Radio_Id_No" runat="server" name="26. Id'd before consulting field guides"
                        type="radio" value="No" />No</td>
            </tr>
            <tr>
                <td id="TD35" runat="server">
                    Notes were made during
                        <input id="RadioNotesDuring" runat="server" name="27. Notes taken during or after observation"
                            type="radio" value="during" />or after observation<input id="RadioNotesAfter" runat="server"
                                name="27. Notes taken during or after observation" type="radio" value="after" /></td>
            </tr>
            <tr>
                <td id="Td14" runat="server" style="height: 31px">
                    How well was the bird seen:
                    <input id="RadioVeryWell" runat="server" name="28. How well seen" type="radio" value="very well" />Very 
                    well
                    <input id="RadioModeratelyWell" runat="server" name="28. How well seen" type="radio"
                        value="Moderately well" />Moderately well
                    <input id="RadioOK" runat="server" name="28. How well seen" type="radio" value="OK" />OK
                    <input id="RadioPoorly" runat="server" name="28. How well seen" type="radio" value="Poorly" />Poorly</td>
            </tr>
            <tr>
                <td id="TD47" runat="server">
                    What books, recordings, web sites or other aids influenced your decision:</td>
            </tr>
            <tr>
                <td id="TD51" runat="server">
                    <input id="GuidesUsed" runat="server" name="29. What influenced your decision" style="width: 575px"
                        type="text" /></td>
            </tr>
            <tr>
                <td id="Td64" runat="server" style="text-align: left">
                    <hr width="462" style="text-align: left" />
                    &nbsp;</td>
            </tr>
        </table>
        
        </asp:Panel>
        <br />
                        &nbsp;<asp:Button ID="ButtonReview" runat="server" OnClick="Button1_Click" Text="Save and Submit" Width="130px" ToolTip="Submit your photo" />
                        &nbsp;
		<asp:Button ID="ButtonExit" runat="server" Text="Exit without saving" Width="130px" OnClick="ButtonExit_Click" CausesValidation="False" />
        <h3>
        <br />
    	    If you click &quot;Save and Submit&quot;, your files are uploaded, a mockup web page is 
            automatically created, you are sent a confirmation email and the AZFO Photo 
            Editors are notified.   Editors are notified.</h3>
		<h3>If you click &quot;Exit without Saving&quot; you are simply returned to the Photo Gallery 
            and nothing is saved or submitted.<br />
        </h3>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/AZListXMLFile.xml" XPath="Birds/Species">
        </asp:XmlDataSource>
        <asp:XmlDataSource ID="XmlDataSourceCounty" runat="server" DataFile="~/App_Data/countyList.xml">
        </asp:XmlDataSource>
        <asp:XmlDataSource ID="XmlDataSourceBandCode" runat="server" DataFile="~/App_Data/AZListXMLFile.xml"
            XPath="Birds/Species"></asp:XmlDataSource>
                            <br />
        <asp:XmlDataSource ID="XmlDataSourceReviewSpecies" runat="server" DataFile="~/App_Data/AZListXMLFile.xml"
            XPath="Birds/Review"></asp:XmlDataSource>
					</div>
				</td>
			</tr>
			<tr>
						<td align='left' valign='top' width='700'>
						<p><i></i>&nbsp;</p>
				</td>
			</tr>
			<tr>
						<td align='left' valign='top' width='700'>
						    &nbsp;</td>
			</tr>
			<tr>
						<td align='left' valign='top' width='700'>
						    &nbsp;</td>
			</tr>
			</table>
     </form>
</asp:Content>


