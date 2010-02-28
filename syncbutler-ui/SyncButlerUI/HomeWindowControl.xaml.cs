﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace SyncButlerUI
{
	/// <summary>
	/// Interaction logic for HomeWindowControl.xaml
	/// </summary>
	public partial class HomeWindowControl : UserControl
	{
		public HomeWindowControl()
		{
			this.InitializeComponent();
		}

			 /// <summary>
    /// Interaction logic for Creating Partnership
    /// </summary>

        private object dummyNode = null;
        public string SelectedImagePath { get; set; }
        void TreeWindow_Loaded(object sender, RoutedEventArgs e)
        {
			foldersItem.Items.Clear();
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
				if(d.IsReady){
				string s = d.Name;
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem.Items.Add(item);
				}
            }

        }

        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = (TreeView)sender;
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);

            if (temp == null)
                return;
            SelectedImagePath = "";
            string temp1 = "";
            string temp2 = "";
            while (true)
            {
                temp1 = temp.Header.ToString();
                if (temp1.Contains(@"\"))
                {
                    temp2 = "";
                }
                SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                if (temp.Parent.GetType().Equals(typeof(TreeView)))
                {
                    break;
                }
                temp = ((TreeViewItem)temp.Parent);
                temp2 = @"\";
            }
            //show user selected path
			//destinationTextBox.Text=SelectedImagePath;
          	sourceTextBox.Text=SelectedImagePath;
			//  MessageBox.Show(SelectedImagePath);
        }
		
		private void goToPartnershipDest(object sender, RoutedEventArgs e){
		    try{
				checkInput();
			PartnershipTempData.sourcePath=sourceTextBox.Text;
		    sourceTextBox.Text=PartnershipTempData.destinationPath;
			VisualStateManager.GoToState(this,"CreatePartnershipState2",false);
		    }catch(Exception ex){
			MessageBox.Show(ex.Message);
			}
		}
		
		/// <summary>
		/// Checks the sourceTextbox for values if its empty or if the directory exists
		/// </summary>
		private void checkInput(){
			   if(sourceTextBox.Text.Length>266){
				throw new Exception("Folder Path is too long");
			}else if(sourceTextBox.Text.Equals("")){
				throw new Exception("Please select a Folder");
			}else if(!Directory.Exists(sourceTextBox.Text)){
				throw new Exception("No Such Folder");
		
			}
			
		}
		
		/// <summary>
		/// go to
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		
		private void goToCreatePartnership(object sender, RoutedEventArgs e){
		   VisualStateManager.GoToState(this,"CreatePartnershipState1",false);
		   sourceTextBox.Text="";
		}
		private void goBackToCreatePartnershipSrc(object sender, RoutedEventArgs e){
		  	try{

			PartnershipTempData.destinationPath=sourceTextBox.Text;
		    sourceTextBox.Text=PartnershipTempData.sourcePath;
			VisualStateManager.GoToState(this,"CreatePartnershipState1",false);
		    }catch(Exception ex){
			MessageBox.Show(ex.Message);
			}
		}
		private void goToCreatePartnershipName(object sender, RoutedEventArgs e){
			try{
			checkInput();
			PartnershipTempData.destinationPath=sourceTextBox.Text;
		    if(PartnershipTempData.destinationPath.Equals(PartnershipTempData.sourcePath)){
			throw new Exception("Same Folders selected: Please pick another Folder");	
			}
			sourceTextBox1.Text=PartnershipTempData.sourcePath;
			destinationTextBox1.Text=PartnershipTempData.destinationPath;
			partnershipNameTextBox.Text=PartnershipTempData.partnershipName;	
			VisualStateManager.GoToState(this,"CreatePartnershipState3",false);
		    }catch(Exception ex){
			MessageBox.Show(ex.Message);
			}	
		}
		
		private void goBackToCreatePartnershipDes(object sender, RoutedEventArgs e){
		   PartnershipTempData.partnershipName=partnershipNameTextBox.Text;
		   destinationTextBox1.Text=PartnershipTempData.destinationPath;
		   VisualStateManager.GoToState(this,"CreatePartnershipState2",false);
		}
		
		private void createPartnership(object sender, RoutedEventArgs e){
		 try{
			if(partnershipNameTextBox.Text.Equals("")){
			throw new Exception("Please input a partnership Name");	
			
			}
			PartnershipTempData.partnershipName=partnershipNameTextBox.Text;
			sourceFolderPath.Text=PartnershipTempData.sourcePath;
			destinationFolderPath.Text=PartnershipTempData.destinationPath;
		    partnerShipName.Text=PartnershipTempData.partnershipName;
			VisualStateManager.GoToState(this,"CreatePartnershipDone1",false);
			sourceTextBox1.Text="";
			destinationTextBox1.Text="";
			sourceTextBox.Text="";
		    PartnershipTempData.clear();
		   }catch(Exception ex){
			MessageBox.Show(ex.Message);
			}	
		}
		
		private void goHome(object sender, RoutedEventArgs e){
				VisualStateManager.GoToState(this,"Home",false);
		}
		
	}
}