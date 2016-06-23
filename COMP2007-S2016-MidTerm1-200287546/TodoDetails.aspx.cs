using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// required for EF DB access
using COMP2007_S2016_MidTerm1_200287546.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

/*
*@File Name: TodoDetails.aspx.cs
*@Author: Alex Nicholls
*@Student Number: 200287546
*@Site: Midterm
*@Date: June 23, 2016
*@Description: This page displays details and allows editing of a todo item.  
*/

namespace COMP2007_S2016_MidTerm1_200287546
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check for postback
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.getTodo();
            }
        }//end of page load

        /**
        * <summary>
        * This method gets the todo data to the fields
        * </summary>
        * 
        * @method getTodo
        * @returns {void}
        */
        protected void getTodo()
        {
            // populate the form with existing data from the database
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // connect to the EF DB
            using (TodoConnection db = new TodoConnection())
            {
                // populate a game object instance with the ID from the URL Parameter
                Todo updatedTodo = (from TodoList in db.Todos 
                                    where TodoList.TodoID == TodoID
                                    select TodoList).FirstOrDefault();

                // map the properties to the form controls
                if (updatedTodo != null)
                {
                    TodoNameTextBox.Text = updatedTodo.TodoName;
                    TodoNotesTextBox.Text = updatedTodo.TodoNotes;
                    CompletedCheckBox.Checked = updatedTodo.Completed.Value;
                }
            }//end of DB
        }//end of getTodo
        
        /**
         * <summary>
         * This method saves the todo data to the table
         * </summary>
         * 
         * @method SaveButton_Click
         * @returns {void}
         */
         protected void SaveButton_Click(object sender, EventArgs e)
        {
            // connect to EF DB
            using (TodoConnection db = new TodoConnection())
            {

                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0)
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // get the current game from EF DB
                    newTodo = (from TodoList in db.Todos
                               where TodoList.TodoID == TodoID
                               select TodoList).FirstOrDefault();
                }

                //set the todo's data
                
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;
                newTodo.Completed = CompletedCheckBox.Checked;
               
                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }

                // run insert in DB
                db.SaveChanges();

                // redirect to the updated game page
                Response.Redirect("~/TodoList.aspx");
            }
        }//end of Save

        /**
         * <summary>
         * This method returns the user to the TodoList page
         * </summary>
         * 
         * @method CancelButton_Click
         * @returns {void}
         */
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // redirect to the list page
            Response.Redirect("~/TodoList.aspx");
        }//end of Cancel
    }//end of class
}//end of file