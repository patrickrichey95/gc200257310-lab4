<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="course.aspx.cs" Inherits="Lab_4.course" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Course Details</h1>
    <h5>All Fields Required</h5>
    <fieldset>
        <label for="ddlDepartment" class="col-sm-2">Department: </label>
        <asp:DropDownList DataTextField="Name" DataValueField="DepartmentID" ID="ddlDepartment" runat="server">
        </asp:DropDownList>
    </fieldset>
    <fieldset>
        <label for="txtName" class="col-sm-2">Course Name: </label>
        <asp:TextBox ID="txtName" runat="server" required MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtCredits" class="col-sm-2">Credits: </label>
        <asp:TextBox ID="txtCredits" runat="server" TextMode="Number" required MaxLength="50" />
    </fieldset>
    
    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>

    <h2>Enrollments</h2>
    <asp:GridView ID="grdEnrollments" runat="server" AutoGenerateColumns="false"
         DataKeyNames="CourseID" OnRowDeleting="grdEnrollments_RowDeleting"
         CssClass="table table-striped table-hover">
        <Columns>
            <asp:BoundField DataField="FirstMidName" HeaderText="First Name" SortExpression="FirstMidName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
            <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" SortExpression="EnrollmentDate" />
            <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" HeaderText="Delete" />
        </Columns>
    </asp:GridView>
</asp:Content>
