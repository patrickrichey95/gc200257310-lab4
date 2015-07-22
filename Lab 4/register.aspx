<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Lab_4.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Register</h1>
    <h5>All Fields Required</h5>
    <div class="form-group-lg">
        <asp:Label ID="lblStatus" runat="server" CssClass="label label-danger"></asp:Label>
    </div>
    <fieldset class="form-group">
        <label for="txtUsername" class="col-sm-2">Username: </label>
        <asp:TextBox ID="txtUsername" runat="server" required MaxLength="50" />
    </fieldset>
    <fieldset class="form-group">
        <label for="txtPassword" class="col-sm-2">Password: </label>
        <asp:TextBox ID="txtPassword" runat="server" required MaxLength="50" TextMode="Password" />
    </fieldset>
    <fieldset class="form-group">
        <label for="txtConfirm" class="col-sm-2">Confirm: </label>
        <asp:TextBox ID="txtConfirm" runat="server" required MaxLength="50" TextMode="Password" />
        <asp:CompareValidator ID="CompareValidatorPass" runat="server" ErrorMessage="Passwords Don't Match" 
            ControlToValidate="txtConfirm" ControlToCompare="txtPassword" Operator="Equal" CssClass="label label-danger"></asp:CompareValidator>
    </fieldset>
    
    <div class="col-sm-offset-2">
        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
    </div>
</asp:Content>
