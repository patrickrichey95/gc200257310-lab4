using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference the EF Models
using Lab_4.Models;
using System.Web.ModelBinding;

namespace Lab_4
{
    public partial class department : System.Web.UI.Page
    {
        Department s = new Department();
        Int32 DepartmentID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if save wasnt clicked AND we have a StudentID in the url
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                GetDepartments();
            }
        }

        protected void GetDepartments()
        {
            //populate form wih existing student record
            Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            try
            {
                //connect to db via EF
                using (comp2007Entities db = new comp2007Entities())
                {
                    Department s = (from objs in db.Departments
                                    where objs.DepartmentID == DepartmentID
                                    select objs).FirstOrDefault();

                    //map the student properties to the form controls
                    txtDepartmentName.Text = s.Name;
                    txtBudget.Text = s.Budget.ToString();

                    //map the student properties to the form controls if we found a match
                    if (s != null)
                    {
                        txtDepartmentName.Text = s.Name;
                        txtBudget.Text = s.Budget.ToString();
                    }

                    //courses - this code goes in the same method that populates the student form but below the existing code that's already in GetDepartments()              
                    var objE = (from c in db.Courses
                                join d in db.Departments on c.DepartmentID equals d.DepartmentID
                                where c.DepartmentID == DepartmentID
                                select new { c.DepartmentID, c.Title, c.Credits });

                    grdCourses.DataSource = objE.ToList();
                    grdCourses.DataBind();
                }
            }
            catch (Exception err)
            {
                Server.Transfer("/error.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //use EG to connect to SQL Server
                using (comp2007Entities db = new comp2007Entities())
                {
                    //use the Student Model to save the new record


                    //check the querystring for an id so we can determine add/update
                    if (Request.QueryString["DepartmentID"] != null)
                    {
                        //get the id from the url
                        DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                        //get the current student from EF
                        s = (from objs in db.Departments
                             where objs.DepartmentID == DepartmentID
                             select objs).FirstOrDefault();
                    }

                    s.Name = txtDepartmentName.Text;
                    s.Budget = Convert.ToDecimal(txtBudget.Text);

                    //call add only if we have no student ID
                    if (DepartmentID == 0)
                    {
                        db.Departments.Add(s);
                    }

                    db.SaveChanges();

                    //redirect
                    Response.Redirect("departments.aspx");
                }
            }
            catch (Exception err)
            {
                Server.Transfer("/error.aspx");
            }
        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get selected record id
            Int32 EnrollmentID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["EnrollmentID"]);

            try
            {
                using (comp2007Entities db = new comp2007Entities())
                {
                    //get selected record
                    Enrollment objE = (from en in db.Enrollments
                                       where en.EnrollmentID == EnrollmentID
                                       select en).FirstOrDefault();

                    //delete
                    db.Enrollments.Remove(objE);
                    db.SaveChanges();

                    //refresh the data on the page
                    GetDepartments();
                }
            }
            catch (Exception err)
            {
                Server.Transfer("/error.aspx");
            }
        }
    }
}