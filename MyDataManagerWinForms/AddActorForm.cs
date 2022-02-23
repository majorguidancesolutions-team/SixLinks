using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyDataManagerDataOperations;
using MyDataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MyDataManagerWinForms.MainForm;

namespace MyDataManagerWinForms
{
	public partial class AddActorForm : Form
	{
		public event PopulateMessageEvent populateMessageVariable;
		private Actor _actor;

		public AddActorForm()
		{
			InitializeComponent();
		}

		public AddActorForm(Actor actor)
		{
			InitializeComponent();
			_actor = actor;
			this.txtActorId.Text = actor.Id.ToString();
			this.txtFirstName.Text = actor.FirstName;
			this.txtLastName.Text = actor.LastName;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			var dataOps = new DataOperations();

			if (string.IsNullOrWhiteSpace(this.txtActorId.Text))
			{
				if (string.IsNullOrWhiteSpace(this.txtFirstName.Text))
				{
					MessageBox.Show("Enter an actor's first name.", "Missing Actor Name", MessageBoxButtons.OK,
										MessageBoxIcon.Exclamation);

					// need to be able to stay on the form to re-enter and click button...
				}

				else if (string.IsNullOrWhiteSpace(this.txtLastName.Text))
				{
					DialogResult userSelection = MessageBox.Show("Does this actor have a last name?", "Missing Actor Name",
																	MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (userSelection == DialogResult.Yes)
					{
						MessageBox.Show("Enter the actor's last name.", "Missing Actor Name", MessageBoxButtons.OK,
											MessageBoxIcon.Exclamation);
					}
					else
					{
						// add the actor
						AddActor(this.txtFirstName.Text, string.Empty);
					}
				}
				else
				{
					AddActor(this.txtFirstName.Text, this.txtLastName.Text);
				}
			}
			else
			{
				dataOps.UpdateActor(this.txtActorId.Text, this.txtFirstName.Text, this.txtLastName.Text);
				if (populateMessageVariable is not null)
				{
					populateMessageVariable.Invoke($"{this.txtFirstName.Text} {this.txtLastName.Text} updated");
				}
			}
			this.Close();
		}

		private void AddActor(string firstName, string lastName)
		{
			// check that the input is not in database
			var dataOps = new DataOperations();

			if (dataOps.CheckExistingActor(firstName, lastName))
			{
				var newActor = new Actor();
				newActor.FirstName = firstName;
				newActor.LastName = lastName;

				DataImporter di = new DataImporter();
				try
				{
					Task.Run(async () => await di.GetNewActor(newActor));
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				if (populateMessageVariable is not null)
				{
					populateMessageVariable.Invoke($"{firstName} {lastName} added");
				}
			}
			else
			{
				MessageBox.Show($"{firstName} {lastName} is already in the database.", "Existing Actor",
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}