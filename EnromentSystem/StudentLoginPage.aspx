<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentLoginPage.aspx.cs" Inherits="StudentLoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/studentLogin.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Styles/main.css") %>" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <div>
                <asp:Image runat="server" ImageUrl="~/Images/INTI_IU_logo.png"/>
            </div>
            <div id="infoBox">
                <p>User ID</p>
                <div>
                    <asp:TextBox ID="txtUserId" runat="server" CssClass="textBox"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator2" 
                        ControlToValidate="txtUserId"
                        ForeColor="red"
                        Display="dynamic"
                        CssClass="validator"
                        runat="server" 
                        ErrorMessage="This field is requited">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="RegularExpressionValidator1" 
                        runat="server" 
                        ControlToValidate="txtUserId"
                        ForeColor="red"
                        Display="dynamic"
                        CssClass="validator"
                        ErrorMessage="Student ID not in required fromat"
                        ValidationExpression="I\d+">
                    </asp:RegularExpressionValidator>
                </div>
                <p>Password</p>
                <div>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textBox"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" 
                        ControlToValidate="txtPassword"
                        ForeColor="red"
                        Display="dynamic"
                        CssClass="validator"
                        runat="server" 
                        ErrorMessage="This field is requited">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator 
                        ID="cvdLoginFall" 
                        ForeColor="red"
                        Display="dynamic"
                        runat="server" 
                        ErrorMessage="Incorrect password, please re-enter"
                        CssClass="validator"
                        OnServerValidate="cvdLoginFall_ServerValidate">
                    </asp:CustomValidator><br />
                    <a href="#" id="forgetPasswordWindows">Forget Password?</a>
                </div>
            </div>
            
            <asp:Button ID="btnLogin" runat="server" Text="Login to Online Enrolment Portal" OnClick="btnLogin_Click" />

            <div class="terms">
                <p>
                    By signing onto this website, you agree to abide by its 
                    <span>
                        <a href="#" id="openModalLink" >
                            Terms of Use
                        </a>
                    </span><br />
                    Violations could lead to restriction of website privileges and/or disciplinary action.
                </p>
            </div>

            <div id="popUpWindows" class="pop-up-windows">
                <div class="windows-contain">
                    <div class="intiLogo"></div>
                    <h1>Term Of Use</h1>
                    <p>
                        1)	It is the responsibility of the students to ensure that the subjects they are enrolling for is part of their programme study plan. 
                        INTI will not be responsible for any subjects enrolled wrongly by the student.
                    </p>
                    <p>
                        2)	Students are encouraged to seek the advice of their Head of Programme and Lecturers before enrolling for their subjects.
                    </p>
                    <p>
                        3)	INTI reserves the absolute right and as it deems fit:
                        <span>
                            i)	To defer, close, or not to commence any subjects, programmes; and/or
                        </span>
                        <span>
                            ii)	To review, change and update subject curriculum in any programmes
                        </span>
                        <span>
                            iii)	Students will be informed about the decision no later by the end of the first week of the semester; and
                        </span>
                        <span>
                            iv)	INTI shall refund fees, deposits or any other fees paid by the student interest-fee.
                        </span>
                    </p>
                    <p>
                        4)	Please read the Terms and Conditions for Enrollment below before proceeding with the enrollment. 
                        Students are to declare online that they have read and agreed to the Terms and Conditions for Enrollment.
                    </p>
                    <h1>Terms and Conditions for Enrollment</h1>
                    <p>
                        1)	Tuition fees and related charges are due in full on or before the start of the academic session and each new session thereafter.
                    </p>
                    <p>
                        2)	Full settlement of semester / term fees is required upon registration or by the start date of semester / 
                        term and according to the due dates for subsequent semesters / terms.
                    </p>
                    <p>
                        3)  Complete your enrollment and pay your fees using one of the following methods:
                        <span>
                            i)	Electronic Fund Transfer
                        </span>
                        <span>
                            ii)	Online Payment with Credit and Debit Cards
                        </span>
                        <span>
                            iii) CUP cards
                        </span>
                        Fee payment by cheque or bankers’ draft can be deposited directly into INTI’s bank account. 
                        Student must upload the payment proof via Online Enrollment and allow at least three (3) business days for student’s account update.
                    </p>
                    <p>
                        4)	It is the responsibility of each student to ensure timely payment of fees and other related charges associated with the respective programme of study.
                    </p>
                    <p>
                        5)	All fees paid (except deposit) are neither refundable nor transferable once the semester has commenced.
                    </p>
                    <p>
                        6)	All payment of refunds shall be made payable to students.
                    </p>
                    <p>
                        7)	All refunds whether of fees, deposits or any other payments shall be free of interest and shall be subject to the right of set-off by INTI against any fees 
                        or whatsoever payments due and owing to INTI.
                    </p>
                    <p>
                        8)	All claims for refund of any moneys (whether fees, deposits or any other payments) shall be made within one (1) month from the date the particular student ceases to be a student of INTI.
                    </p>
                    <p>
                        9)	There shall be no refund (save and except refundable deposits) if a student is expelled from INTI due to academic misconduct or disciplinary behaviour.
                    </p>
                    <p>
                        10)	A late payment charge of Ringgit Malaysia Three Hundred (RM300) will be imposed commencing from the second week of the semester / term. 
                        INTI reserves the right to review the status of the student and to take such necessary action as it deems fit, including but not limited to the deregistration of subjects enrolled (auto drop), 
                        barring the student from classes and facilities, suspension, withholding of all examination results, certificates and records of the student.
                    </p>
                    <asp:Button 
                        ID="btnPopWindowsClose"
                        runat="server" 
                        Text="Close" 
                        OnClientClick="document.getElementById('popUpWindows').style.display='none'; return false;" />
                </div>
            </div>

            <div id="forgetPassword" class="pop-up-windows">
                <div class="windows-contain">
                    <div class="intiLogo"></div>
                    <div class="container-in-forget-password-page">
                        <h1>Forgot Your Password?</h1>
                        <h2>User Id</h2>
                        <asp:TextBox ID="txtResetPasswordId" runat="server" CssClass="textBox"></asp:TextBox><br />
                        <asp:RequiredFieldValidator 
                            ID="rfvResetPasswordId" 
                            ControlToValidate="txtResetPasswordId"
                            ForeColor="red"
                            Display="dynamic"
                            CssClass="validator"
                            runat="server" 
                            ErrorMessage="This field is requited">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator 
                            ID="revResetPasswordId" 
                            runat="server" 
                            ControlToValidate="txtResetPasswordId"
                            ForeColor="red"
                            Display="dynamic"
                            CssClass="validator"
                            ErrorMessage="Student ID not in required fromat"
                            ValidationExpression="I\d+">
                        </asp:RegularExpressionValidator>
                        <h2>Verification Code</h2>
                        <asp:TextBox ID="txtVerificationCode" runat="server" CssClass="textBox"></asp:TextBox><br />
                        <asp:Button ID="bthGetVerificationCode" runat="server" Text="Get Verification Code" OnClick="bthGetVerificationCode_Click" />
                        <asp:RequiredFieldValidator 
                            ID="RequiredFieldValidator4" 
                            ControlToValidate="txtVerificationCode"
                            ForeColor="red"
                            Display="dynamic"
                            CssClass="validator"
                            runat="server" 
                            ErrorMessage="This field is requited">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:Button ID="btnGetTemporaryPassword" runat="server" Text="Get Temporary Password" OnClick="btnGetTemporaryPassword_Click"/>
                    <asp:Button 
                        ID="btnExitForgetWindows"
                        runat="server" 
                        Text="Close" 
                        OnClientClick="document.getElementById('forgetPassword').style.display='none'; return false;" />
                </div>
            </div>
        </div>
    </form>
    <script src="Scripts/LoginPagePopUpWindows.js"></script>
</body>
</html>
