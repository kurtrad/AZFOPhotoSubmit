<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/AZFOPhotoSubmit.Master" AutoEventWireup="true" CodeBehind="ABCSubmitMain.aspx.cs" Inherits="AZFOPhotoSubmit.ABCSubmitMain" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
     <form id="form1" runat="server">   
    <table id="TABLE1" runat="server" border="0" cellpadding="3" cellspacing="2" 
        width="700" align="center">
        <tr>
            <td runat="server" style="font-weight: bold; color: teal; height: 29px">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/az_sta3.gif" />
            </td>
        </tr>
        <tr>
            <td runat="server" style="font-weight: bold; color: teal; height: 29px">
                Arizona Bird Committee Review Species - Report Form&nbsp;</td>
        </tr>
        <tr>
            <td id="Td1" runat="server" 
                style="font-weight: bold; color: teal; height: 29px">
                Arizona Bird Committee Managing Secretary<br />
                Gary H. Rosenberg (Secretary) P.O. Box 91856 Tucson, AZ 85752-1856
                <br />
                e-mail:
                <a href="mailto:ghrosenberg@comcast.net">ghrosenberg@comcast.net </a>
            </td>
        </tr>
        <tr>
            <td id="Td3" runat="server" style="height: 21px">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                <asp:Label ID="lblMessage" runat="server" Width="850px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="Td7" runat="server">
                <asp:DropDownList ID="DropDownListOfSpecies" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="XmlDataSource1" 
                    DataTextField="EnglishName" DataValueField="LatinName" Font-Bold="False" 
                    onselectedindexchanged="DropDownListOfSpecies_SelectedIndexChanged" 
                    ToolTip="Complete list of ABC review and sketch details species" Width="350px">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="DropDownListOfSpecies" 
                    ErrorMessage="Please select a species or use other" Operator="NotEqual" 
                    ValueToCompare="Selectus"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td id="Td77" runat="server">
                <asp:Label ID="LabelOther" runat="server" 
                    Text="Other species if not listed above: &nbsp;" Visible="False"></asp:Label>
                <asp:TextBox ID="TextBoxOther" runat="server" Visible="False" Width="169px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorOther" runat="server" 
                    ControlToValidate="TextBoxOther" ErrorMessage="Please enter a species name"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td id="Td37" runat="server" style="height: 25px">
                Date observed 
                (mm/dd/yyyy format):&nbsp;
                <asp:TextBox ID="TextBoxObsDate" runat="server"></asp:TextBox>
                <asp:MaskedEditExtender ID="TextBoxObsDate_MaskedEditExtender" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="TextBoxObsDate">
                </asp:MaskedEditExtender>
                <asp:CalendarExtender ID="TextBoxObsDate_CalendarExtender" runat="server" 
                    Enabled="True" PopupButtonID="DatePicker" TargetControlID="TextBoxObsDate">
                </asp:CalendarExtender>
                <asp:Image ID="DatePicker" runat="server" ImageUrl="~/images/calendar.gif" 
                    Height="23px" Width="25px" />
                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" 
                    ControlExtender="TextBoxObsDate_MaskedEditExtender" 
                    ControlToValidate="TextBoxObsDate" EmptyValueMessage="Date is Required" 
                    ErrorMessage="Please enter a valid date (mm/dd/yyyy format)" 
                    InvalidValueMessage="Please enter a valid date (mm/dd/yyyy format)" 
                    IsValidEmpty="False"></asp:MaskedEditValidator>
            </td>
        </tr>
        <tr>
            <td id="Td38" runat="server">
                &nbsp;</td>
        </tr>
        <tr>
            <td id="Td66" runat="server">
                Initial time of observation:
                <asp:TextBox ID="TextBoxTimeObserved" runat="server" Width="142px"></asp:TextBox>
                &nbsp;Duration:
                <input id="Duration" runat="server" name="12. Duration of observation" 
                    size="21" type="text" /></td>
        </tr>
        <tr>
            <td id="TD28" runat="server" style="height: 30px;">
                How many birds observed:
                <input id="NumberObserved" runat="server" name="05. Number of individuals" 
                    size="3" type="text" value="1" /> Age:
                <input id="Age" runat="server" name="06. Age" size="24" type="text" 
                    value="adult, juvenile, etc." /> Sex:
                <input id="Sex" runat="server" name="07. Sex" style="width: 84px" type="text" 
                    value="unknown" /></td>
        </tr>
        <tr>
            <td id="TD55" runat="server">
                Exact location:
            </td>
        </tr>
        <tr>
            <td id="TD27" runat="server">
                <textarea id="Location" runat="server" cols="20" name="08. Exact location" 
                    rows="3" style="width: 575px">Exact address or specific details, such as distance and direction from nearest landmark</textarea></td>
        </tr>
        <tr>
            <td id="TD100" runat="server">
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="County" ErrorMessage="Please Select a County" 
                    Operator="NotEqual" ValueToCompare="Select County"></asp:CompareValidator>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="City" 
                    ErrorMessage="Please enter a city or the nearest landmark"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td id="TD34" runat="server">
                County:
                <asp:DropDownList ID="County" runat="server" Width="120px">
                    <asp:ListItem>Select County</asp:ListItem>
                    <asp:ListItem>Apache</asp:ListItem>
                    <asp:ListItem>Cochise</asp:ListItem>
                    <asp:ListItem>Coconino</asp:ListItem>
                    <asp:ListItem>Gila</asp:ListItem>
                    <asp:ListItem>Graham</asp:ListItem>
                    <asp:ListItem>Greenlee</asp:ListItem>
                    <asp:ListItem>LaPaz</asp:ListItem>
                    <asp:ListItem>Maricopa</asp:ListItem>
                    <asp:ListItem>Mohave</asp:ListItem>
                    <asp:ListItem>Navajo</asp:ListItem>
                    <asp:ListItem>Pima</asp:ListItem>
                    <asp:ListItem>Pinal</asp:ListItem>
                    <asp:ListItem>Santa Cruz</asp:ListItem>
                    <asp:ListItem>Yavapai</asp:ListItem>
                    <asp:ListItem>Yuma</asp:ListItem>
                </asp:DropDownList>
                &nbsp; Nearest city:
                <input id="City" runat="server" name="10. City" style="width: 133px" 
                    type="text" /> Elevation:
                <input id="Elevation" runat="server" name="11. Elevation" style="width: 114px" 
                    type="text" /></td>
        </tr>
        <tr>
            <td id="TD59" runat="server" style="height: 24px;">
                Distance from bird:
                <input id="Distance" runat="server" name="13. Distance from bird" size="18" 
                    type="text" value="paced, estimated, other" /> Optical equipment:
                <input id="Optics" runat="server" name="14. Optical equipment" 
                    style="width: 199px" type="text" />
            </td>
        </tr>
        <tr>
            <td id="TD52" runat="server">
                Relationship of sun/observer/bird:
                <input id="Sun" runat="server" name="15. Relationship of sun/observer/bird" 
                    style="width: 365px" type="text" />
            </td>
        </tr>
        <tr>
            <td id="TD44" runat="server">
                Habitat:
                <input id="Habitat" runat="server" name="16. Habitat " style="width: 520px" 
                    type="text" value="desert scrub, oakwoodland, etc." />
            </td>
        </tr>
        <tr>
            <td id="TD6" runat="server">
                Initial observer(s):
            </td>
        </tr>
        <tr>
            <td id="TD40" runat="server" style="height: 59px;">
                <textarea id="InitialObserver" runat="server" cols="20" 
                    name="17. Others who saw bird" rows="1" style="width: 575px; height: 50px">Please provide name and contact information (if known)</textarea></td>
        </tr>
        <tr>
            <td id="TD20" runat="server">
                Others who have independently ID&#39;d bird:
            </td>
        </tr>
        <tr>
            <td id="TD5" runat="server">
                <textarea id="OtherObservers" runat="server" cols="20" 
                    name="18. Others who independently id'd bird" rows="1" 
                    style="width: 575px; height: 50px">Please provide name and contact information (if known)</textarea></td>
        </tr>
        <tr>
            <td id="TD42" runat="server">
                Anybody known to disagree with ID:
            </td>
        </tr>
        <tr>
            <td id="TD9" runat="server" style="height: 72px;">
                <textarea id="Disagree" runat="server" cols="20" 
                    name="19. Anybody known to disagree?" rows="1" 
                    style="width: 575px; height: 50px">Please provide name and contact information (if known)</textarea></td>
        </tr>
        <tr>
            <td id="TD19" runat="server">
                Describe in detail the features noted. <font face="Arial" size="2">
                <font face="Times New Roman" size="3">size, shape, plumage characters, eye 
                color, legs,<br />
                bill, and any other unique features:</font>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="Description" 
                    ErrorMessage="Please enter a detailed description"></asp:RequiredFieldValidator>
                </font>
            </td>
        </tr>
        <tr>
            <td id="TD8" runat="server">
                <textarea id="Description" runat="server" cols="20" name="20. Description" 
                    rows="1" style="width: 575px; height: 100px"></textarea></td>
        </tr>
        <tr>
            <td id="TD26" runat="server">
                Vocalization:
            </td>
        </tr>
        <tr>
            <td id="TD33" runat="server">
                <input id="Vocalization" runat="server" name="21. Vocalizations" 
                    style="width: 575px" type="text" /></td>
        </tr>
        <tr>
            <td id="TD41" runat="server">
                Behavior:
            </td>
        </tr>
        <tr>
            <td id="TD10" runat="server" style="height: 72px;">
                <textarea id="Behavior" runat="server" cols="20" name="22. Behavior" rows="1" 
                    style="width: 575px; height: 50px"></textarea></td>
        </tr>
        <tr>
            <td id="TD16" runat="server">
                What similar species were considered and why were they eliminated:
            </td>
        </tr>
        <tr>
            <td id="TD36" runat="server">
                <textarea id="SimilarSpecies" runat="server" cols="20" 
                    name="23. Species eliminated and why" rows="1" 
                    style="width: 575px; height: 50px"></textarea></td>
        </tr>
        <tr>
            <td id="TD54" runat="server">
                Explain previous experience with this species:
            </td>
        </tr>
        <tr>
            <td id="TD49" runat="server">
                <textarea id="Experience" runat="server" cols="20" 
                    name="24. Previous experience with this species" rows="1" 
                    style="width: 575px; height: 50px"></textarea></td>
        </tr>
        <tr>
            <td id="TD12" runat="server">
                Explain previous experience with similar species:
            </td>
        </tr>
        <tr>
            <td id="TD58" runat="server">
                <textarea id="ExperienceSimilar" runat="server" cols="20" 
                    name="25. Previous experience with similar species" rows="1" 
                    style="height: 50px; width: 575px;"></textarea></td>
        </tr>
        <tr>
            <td id="TD50" runat="server">
                Did you identify this bird before consulting any field guides:
                <input id="Radio_Id_Yes" runat="server" checked="true" 
                    name="26. Id'd before consulting field guides" type="radio" value="Yes" /> 
                Yes
                <input id="Radio_Id_No" runat="server" 
                    name="26. Id'd before consulting field guides" type="radio" value="No" /> No
            </td>
        </tr>
        <tr>
            <td id="TD35" runat="server">
                Notes were made during
                <input id="RadioNotesDuring" runat="server" checked="true" 
                    name="27. Notes taken during or after observation" type="radio" 
                    value="during" />or after observation<input id="RadioNotesAfter" 
                    runat="server" name="27. Notes taken during or after observation" type="radio" 
                    value="after" />
            </td>
        </tr>
        <tr>
            <td id="TD13" runat="server" style="height: 31px">
                How well was the bird seen:
                <input id="RadioVeryWell" runat="server" checked="true" 
                    name="28. How well seen" type="radio" value="very well" /> Very well
                <input id="RadioModeratelyWell" runat="server" name="28. How well seen" 
                    type="radio" value="Moderately well" /> Moderately well
                <input id="RadioOK" runat="server" name="28. How well seen" type="radio" 
                    value="OK" /> OK
                <input id="RadioPoorly" runat="server" name="28. How well seen" type="radio" 
                    value="Poorly" /> Poorly
            </td>
        </tr>
        <tr>
            <td id="TD47" runat="server">
                What guides or aids influenced your decision:
            </td>
        </tr>
        <tr>
            <td id="TD51" runat="server">
                <input id="GuidesUsed" runat="server" name="29. What influenced your decision" 
                    style="width: 575px" type="text" /></td>
        </tr>
        <tr>
            <td id="TD39" runat="server">
                Indicate any materials being submitted:
            </td>
        </tr>
        <tr>
            <td id="TD31" runat="server">
                <textarea id="Materials" runat="server" cols="20" 
                    name="30. Materials submitted" rows="1" style="width: 575px; height: 50px">e.g. Photos(s), video, audio etc.</textarea></td>
        </tr>
        <tr>
            <td id="TD15" runat="server" style="height: 27px;">
                Was the bird photographed:<input id="PhotoYes" runat="server" checked="true" 
                    name="31. photographed" type="radio" value="Yes" /> Yes
                <input id="PhotoNo" runat="server" name="31. photographed" type="radio" 
                    value="No" /> No
            </td>
        </tr>
        <tr>
            <td id="TDCheckBoxOtherSubmitter" runat="server">
                <asp:CheckBox ID="CheckBoxOtherSubmitter" runat="server" AutoPostBack="True" 
                    oncheckedchanged="CheckBoxOtherSubmitter_CheckedChanged" 
                    Text="Check here if you are submitting this record on behalf of someone else." />
            </td>
        </tr>
        <tr>
            <td id="TDLabelOtherSubmitter" runat="server">
                <asp:Label ID="LabelOtherSubmitter" runat="server" 
                    Text="Individual you are submitting this record for." Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="TDLabelOtherSubmitter2" runat="server">
                <asp:TextBox ID="TextBoxOnBehalfOf" runat="server" Visible="False" 
                    Width="570px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td id="TDTextAreaOther" runat="server">
                <br />
                <textarea id="TextAreaOtherSubmitter" runat="server" cols="20" 
                    name="OtherSubmitter" rows="1" style="width: 575px; height: 50px" 
                    visible="False">Details about submitting this record and why</textarea></td>
        </tr>
        <tr>
            <td id="TD32" runat="server" style="height: 19px;">
                Attach up to 4 items, photos, audio, video etc.
            </td>
        </tr>
        <tr>
            <td id="TD29" runat="server" style="height: 24px;">
                Select at least one image file to upload:
                <br />
                &nbsp; &nbsp; You may submit up to four images<br />
                &nbsp; &nbsp; Recommended file size: No larger than 300 kb, 1000x800 pixels, jpeg format<br />
                &nbsp; &nbsp; Absolute file size limit:&nbsp; 4 Mb per image<br />
                &nbsp; &nbsp; Only images types of .gif, .jpeg, .jpg, .bmp and .png are allowed.&nbsp;<br />
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" Width="312px" />
                <asp:Label ID="LabelFileType1" runat="server" ForeColor="Red" Text="Label" 
                    Visible="False" Width="224px"></asp:Label>
                <br />
                <asp:FileUpload ID="FileUpload2" runat="server" Width="312px" />
                <asp:Label ID="LabelFileType2" runat="server" ForeColor="Red" Text="Label" 
                    Visible="False" Width="224px"></asp:Label>
                <br />
                <asp:FileUpload ID="FileUpload3" runat="server" Width="312px" />
                <asp:Label ID="LabelFileType3" runat="server" ForeColor="Red" Text="Label" 
                    Visible="False" Width="224px"></asp:Label>
                <br />
                <asp:FileUpload ID="FileUpload4" runat="server" Width="312px" />
                <asp:Label ID="LabelFileType4" runat="server" ForeColor="Red" Text="Label" 
                    Visible="False" Width="224px"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td id="TD57" runat="server" style="height: 19px;">
                Your First Name: <span style="color: #cc0000">*
                <asp:TextBox ID="TextBoxFirstName" runat="server" Width="150px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="TextBoxFirstName" 
                    ErrorMessage="Please enter your first name"></asp:RequiredFieldValidator>
                </span>
            </td>
        </tr>
        <tr>
            <td id="TD60" runat="server">
                Your Last Name: <span style="color: #cc0000">*</span> &nbsp;<asp:TextBox 
                    ID="TextBoxLastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="TextBoxLastName" ErrorMessage="Please enter your last name"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td id="Td24" runat="server">
                Your Email Address: <span style="color: #cc0000">*
                <asp:TextBox ID="TextBoxEmailAddress" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="TextBoxEmailAddress" ErrorMessage="email address required"></asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                    runat="server" ControlToValidate="TextBoxEmailAddress" 
                    ErrorMessage="Please enter a valid email address" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </span>
            </td>
        </tr>
        <tr>
            <td id="TD62" runat="server">
                &nbsp;</td>
        </tr>
        <tr>
            <td id="TD63" runat="server">
                &nbsp;<asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click1" Text="Send" 
                    Width="93px" />
                &nbsp; Click send to submit the report: by including your email, you will immediately 
                be cc&#39;d a copy of this report</td>
        </tr>
        <tr>
            <td id="Td64" runat="server" style="text-align: left">
                <hr style="text-align: left" width="462" />
                &nbsp;<asp:XmlDataSource ID="XmlDataSource1" runat="server" 
                    DataFile="~/App_Data/AZListXMLReview.xml" XPath="Birds/Species">
                </asp:XmlDataSource>
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
            </td>
        </tr>
        <tr>
            <td id="Td65" runat="server" style="height: 19px">
                | 
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="http://www.azfo.org/">Arizona Field Ornithologists</asp:HyperLink>
                |
                <asp:HyperLink ID="HyperLink2" runat="server" 
                    NavigateUrl="http://azfo.org/ArizonaBirdCommittee/index.html">Arizona Bird Committee</asp:HyperLink>
                |
                <asp:HyperLink ID="HyperLink3" runat="server" 
                    NavigateUrl="http://www.mexicobirding.com/arizona/az_select.html">Unofficial Arizona County Lists</asp:HyperLink>
                |</td>
        </tr>
    </table>
     </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <p>
        &nbsp;</p>
</asp:Content>
<asp:Content ID="Content5" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
</asp:Content>

