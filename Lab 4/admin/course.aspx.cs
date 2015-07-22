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
    public partial class course : System.Web.UI.Page
    {
        Course s = new Course();
        Int32 CourseID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if save wasnt clicked AND we have a StudentID in the url
            if ((!IsPostBack))
            {
                try
                {
                    using (comp2007Entities db = new comp2007Entities())
                    {
                        var li = (from d in db.Departments orderby d.DepartmentID select d);

                        ddlDepartment.DataSource = li.ToList();
                        ddlDepartment.DataBind();
                    }
                    if (Request.QueryString.Count > 0)
                    {
                        GetCourse();
                    }
                }
                catch(Exception err)
                {
                    Server.Transfer("/error.aspx");
                }
            }
        }

        protected void GetCourse()
        {
            //populate form wih existing student record
            Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

            try
            {
                //connect to db via EF
                using (comp2007Entities db = new comp2007Entities())
                {
                    Course s = (from objs in db.Courses
                                where objs.CourseID == CourseID
                                select objs).FirstOrDefault();

                    //map the student properties to the form controls if we found a match
                    if (s != null)
                    {
                        txtName.Text = s.Title;
                        txtCredits.Text = s.Credits.ToString();
                        ddlDepartment.SelectedValue = s.DepartmentID.ToString();
                    }

                    var objE = (from c in db.Courses
                                join en in db.Enrollments on c.CourseID equals en.CourseID
                                join st in db.Students on en.StudentID equals st.StudentID
                                where en.CourseID == CourseID
                                select new { st.FirstMidName, st.LastName, st.EnrollmentDate, c.CourseID });

                    grdEnrollments.DataSource = objE.ToList();
                    grdEnrollments.DataBind();
                }
            }
            catch(Exception err)
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
                    //use the Student model to save the new record
                    Course c = new Course();

                    //check the querystring for an id so we can determine add/update
                    if (!String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                    {
                        //get the id from the url
                        CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

                        //get the current student from EF
                        c = (from objs in db.Courses
                             where objs.CourseID == CourseID
                             select objs).FirstOrDefault();
                    }

                    c.Title = txtName.Text;
                    c.Credits = Convert.ToInt32(txtCredits.Text);
                    c.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

                    //call add only if we have no student ID
                    if (CourseID == 0)
                    {
                        db.Courses.Add(c);
                    }

                    //run the update or insert
                    db.SaveChanges();

                    //redirect
                    Response.Redirect("courses.aspx");
                }
            }
            catch (Exception err)
            {
                Server.Transfer("/error.aspx");
            }
        }

        protected void grdEnrollments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store which row was clicked
            Int32 selectedRow = e.RowIndex;

            //get the selected StudentID using the grid's Data Key collection
            Int32 EnrollmentID = Convert.ToInt32(grdEnrollments.DataKeys[selectedRow].Values["EnrollmentID"]);

            try
            {
                //use EF to remove the selected student from the db
                using (comp2007Entities db = new comp2007Entities())
                {
                    Enrollment s = (from objs in db.Enrollments
                                    where objs.EnrollmentID == EnrollmentID
                                    select objs).FirstOrDefault();

                    //do the delete
                    db.Enrollments.Remove(s);
                    db.SaveChanges();
                }

                //refresh the grid
                GetCourse();
            }
            catch (Exception err)
            {
                Server.Transfer("/error.aspx");
            }
        }
    }
}