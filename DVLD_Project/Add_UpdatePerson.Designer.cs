using DVLDBusinessLayer;
using System.Data;

namespace DVLD_Project
{
    partial class Add_UpdatePerson
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPerID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.linklbl_Remove_Img = new System.Windows.Forms.LinkLabel();
            this.linklblSetImg = new System.Windows.Forms.LinkLabel();
            this.panelImage = new System.Windows.Forms.Panel();
            this.CB_COUNTRY = new System.Windows.Forms.ComboBox();
            this.TBPHONE = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel9 = new System.Windows.Forms.Panel();
            this.CB_GENDER = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TB_ADDRESS = new System.Windows.Forms.TextBox();
            this.TBEMAIL = new System.Windows.Forms.TextBox();
            this.TBNATNUB = new System.Windows.Forms.TextBox();
            this.TB_LN = new System.Windows.Forms.TextBox();
            this.TB_TN = new System.Windows.Forms.TextBox();
            this.TB_SN = new System.Windows.Forms.TextBox();
            this.TB_FN = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Firdt = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Lbl_Add_Edit = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Person ID:";
            // 
            // lblPerID
            // 
            this.lblPerID.AutoSize = true;
            this.lblPerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerID.Location = new System.Drawing.Point(310, 80);
            this.lblPerID.Name = "lblPerID";
            this.lblPerID.Size = new System.Drawing.Size(63, 31);
            this.lblPerID.TabIndex = 1;
            this.lblPerID.Text = "N/A";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.linklbl_Remove_Img);
            this.panel1.Controls.Add(this.linklblSetImg);
            this.panel1.Controls.Add(this.panelImage);
            this.panel1.Controls.Add(this.CB_COUNTRY);
            this.panel1.Controls.Add(this.TBPHONE);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.CB_GENDER);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.TB_ADDRESS);
            this.panel1.Controls.Add(this.TBEMAIL);
            this.panel1.Controls.Add(this.TBNATNUB);
            this.panel1.Controls.Add(this.TB_LN);
            this.panel1.Controls.Add(this.TB_TN);
            this.panel1.Controls.Add(this.TB_SN);
            this.panel1.Controls.Add(this.TB_FN);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.Firdt);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(10, 135);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1603, 677);
            this.panel1.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::DVLD_Project.Properties.Resources.Save_32_1_;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(969, 586);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(226, 67);
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32_1_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(694, 586);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(226, 67);
            this.btnClose.TabIndex = 41;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // linklbl_Remove_Img
            // 
            this.linklbl_Remove_Img.AutoSize = true;
            this.linklbl_Remove_Img.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklbl_Remove_Img.LinkColor = System.Drawing.Color.Red;
            this.linklbl_Remove_Img.Location = new System.Drawing.Point(1327, 516);
            this.linklbl_Remove_Img.Name = "linklbl_Remove_Img";
            this.linklbl_Remove_Img.Size = new System.Drawing.Size(115, 31);
            this.linklbl_Remove_Img.TabIndex = 40;
            this.linklbl_Remove_Img.TabStop = true;
            this.linklbl_Remove_Img.Text = "Remove";
            this.linklbl_Remove_Img.Visible = false;
            this.linklbl_Remove_Img.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklbl_Remove_Img_LinkClicked);
            // 
            // linklblSetImg
            // 
            this.linklblSetImg.AutoSize = true;
            this.linklblSetImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklblSetImg.Location = new System.Drawing.Point(1315, 458);
            this.linklblSetImg.Name = "linklblSetImg";
            this.linklblSetImg.Size = new System.Drawing.Size(137, 31);
            this.linklblSetImg.TabIndex = 39;
            this.linklblSetImg.TabStop = true;
            this.linklblSetImg.Text = "Set Image";
            this.linklblSetImg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblSetImg_LinkClicked);
            // 
            // panelImage
            // 
            this.panelImage.BackgroundImage = global::DVLD_Project.Properties.Resources.Male_512;
            this.panelImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelImage.Location = new System.Drawing.Point(1227, 157);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(305, 276);
            this.panelImage.TabIndex = 38;
            // 
            // CB_COUNTRY
            // 
            this.CB_COUNTRY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_COUNTRY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_COUNTRY.FormattingEnabled = true;
            this.CB_COUNTRY.Location = new System.Drawing.Point(920, 315);
            this.CB_COUNTRY.Name = "CB_COUNTRY";
            this.CB_COUNTRY.Size = new System.Drawing.Size(275, 39);
            this.CB_COUNTRY.TabIndex = 37;
            // 
            // TBPHONE
            // 
            this.TBPHONE.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBPHONE.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBPHONE.Location = new System.Drawing.Point(915, 236);
            this.TBPHONE.Name = "TBPHONE";
            this.TBPHONE.Size = new System.Drawing.Size(280, 31);
            this.TBPHONE.TabIndex = 36;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 7.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(915, 157);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(280, 26);
            this.dateTimePicker1.TabIndex = 35;
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = global::DVLD_Project.Properties.Resources.Country_32_1_;
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel9.Location = new System.Drawing.Point(851, 313);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(32, 32);
            this.panel9.TabIndex = 34;
            // 
            // CB_GENDER
            // 
            this.CB_GENDER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_GENDER.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_GENDER.FormattingEnabled = true;
            this.CB_GENDER.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Croissant"});
            this.CB_GENDER.Location = new System.Drawing.Point(296, 228);
            this.CB_GENDER.Name = "CB_GENDER";
            this.CB_GENDER.Size = new System.Drawing.Size(275, 39);
            this.CB_GENDER.TabIndex = 33;
            this.CB_GENDER.SelectedIndexChanged += new System.EventHandler(this.CB_GENDER_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(664, 315);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 31);
            this.label12.TabIndex = 32;
            this.label12.Text = "Country:";
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::DVLD_Project.Properties.Resources.Phone_32_1_;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Location = new System.Drawing.Point(851, 235);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(32, 32);
            this.panel7.TabIndex = 31;
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = global::DVLD_Project.Properties.Resources.Calendar_32_1_;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Location = new System.Drawing.Point(851, 157);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(32, 32);
            this.panel8.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(684, 235);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 31);
            this.label10.TabIndex = 29;
            this.label10.Text = "Phone:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(597, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(193, 31);
            this.label11.TabIndex = 28;
            this.label11.Text = "Date Of Birth:";
            // 
            // TB_ADDRESS
            // 
            this.TB_ADDRESS.Location = new System.Drawing.Point(296, 401);
            this.TB_ADDRESS.Multiline = true;
            this.TB_ADDRESS.Name = "TB_ADDRESS";
            this.TB_ADDRESS.Size = new System.Drawing.Size(899, 146);
            this.TB_ADDRESS.TabIndex = 27;
            // 
            // TBEMAIL
            // 
            this.TBEMAIL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBEMAIL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBEMAIL.Location = new System.Drawing.Point(291, 314);
            this.TBEMAIL.Name = "TBEMAIL";
            this.TBEMAIL.Size = new System.Drawing.Size(280, 31);
            this.TBEMAIL.TabIndex = 24;
            // 
            // TBNATNUB
            // 
            this.TBNATNUB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBNATNUB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBNATNUB.Location = new System.Drawing.Point(291, 157);
            this.TBNATNUB.Name = "TBNATNUB";
            this.TBNATNUB.Size = new System.Drawing.Size(280, 31);
            this.TBNATNUB.TabIndex = 23;
            this.TBNATNUB.Validating += new System.ComponentModel.CancelEventHandler(this.TBNATNUB_Validating);
            // 
            // TB_LN
            // 
            this.TB_LN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_LN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_LN.Location = new System.Drawing.Point(1227, 79);
            this.TB_LN.Name = "TB_LN";
            this.TB_LN.Size = new System.Drawing.Size(280, 31);
            this.TB_LN.TabIndex = 22;
            // 
            // TB_TN
            // 
            this.TB_TN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_TN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_TN.Location = new System.Drawing.Point(915, 79);
            this.TB_TN.Name = "TB_TN";
            this.TB_TN.Size = new System.Drawing.Size(280, 31);
            this.TB_TN.TabIndex = 21;
            // 
            // TB_SN
            // 
            this.TB_SN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_SN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_SN.Location = new System.Drawing.Point(603, 79);
            this.TB_SN.Name = "TB_SN";
            this.TB_SN.Size = new System.Drawing.Size(280, 31);
            this.TB_SN.TabIndex = 20;
            // 
            // TB_FN
            // 
            this.TB_FN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_FN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_FN.Location = new System.Drawing.Point(291, 79);
            this.TB_FN.Name = "TB_FN";
            this.TB_FN.Size = new System.Drawing.Size(280, 31);
            this.TB_FN.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1329, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 29);
            this.label9.TabIndex = 18;
            this.label9.Text = "Last";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1024, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 29);
            this.label8.TabIndex = 17;
            this.label8.Text = "Third";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(697, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 29);
            this.label7.TabIndex = 16;
            this.label7.Text = "Second";
            // 
            // Firdt
            // 
            this.Firdt.AutoSize = true;
            this.Firdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Firdt.Location = new System.Drawing.Point(413, 24);
            this.Firdt.Name = "Firdt";
            this.Firdt.Size = new System.Drawing.Size(60, 29);
            this.Firdt.TabIndex = 15;
            this.Firdt.Text = "First";
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::DVLD_Project.Properties.Resources.transparent_history_icon_5db77109ae1843_2334147615723031137131;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Location = new System.Drawing.Point(213, 157);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(32, 32);
            this.panel6.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::DVLD_Project.Properties.Resources.Email_32_1_;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(213, 314);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(32, 32);
            this.panel4.TabIndex = 13;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::DVLD_Project.Properties.Resources.Address_32;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Location = new System.Drawing.Point(213, 393);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(32, 32);
            this.panel5.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::DVLD_Project.Properties.Resources.Man_32_1_;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(213, 235);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(32, 32);
            this.panel3.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::DVLD_Project.Properties.Resources.png_transparent_computer_icons_user_profile_social_web_others_blue_social_media_desktop_wallpaper_thumbnail;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(213, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(32, 32);
            this.panel2.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(30, 394);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 31);
            this.label6.TabIndex = 11;
            this.label6.Text = "Address:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 315);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 31);
            this.label5.TabIndex = 10;
            this.label5.Text = "Email:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 31);
            this.label4.TabIndex = 9;
            this.label4.Text = "Gender:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "National No:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 31);
            this.label2.TabIndex = 7;
            this.label2.Text = "Name:";
            // 
            // Lbl_Add_Edit
            // 
            this.Lbl_Add_Edit.AutoSize = true;
            this.Lbl_Add_Edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Add_Edit.ForeColor = System.Drawing.Color.Red;
            this.Lbl_Add_Edit.Location = new System.Drawing.Point(603, 21);
            this.Lbl_Add_Edit.Name = "Lbl_Add_Edit";
            this.Lbl_Add_Edit.Size = new System.Drawing.Size(386, 55);
            this.Lbl_Add_Edit.TabIndex = 14;
            this.Lbl_Add_Edit.Text = "Add New Person";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel11
            // 
            this.panel11.BackgroundImage = global::DVLD_Project.Properties.Resources.png_transparent_computer_icons_user_profile_social_web_others_blue_social_media_desktop_wallpaper_thumbnail;
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Location = new System.Drawing.Point(223, 80);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(32, 32);
            this.panel11.TabIndex = 13;
            // 
            // Add_UpdatePerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Lbl_Add_Edit);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblPerID);
            this.Controls.Add(this.label1);
            this.Name = "Add_UpdatePerson";
            this.Size = new System.Drawing.Size(1618, 811);
            this.Load += new System.EventHandler(this.Add_UpdatePerson_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPerID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_FN;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label Firdt;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TB_LN;
        private System.Windows.Forms.TextBox TB_TN;
        private System.Windows.Forms.TextBox TB_SN;
        private System.Windows.Forms.TextBox TB_ADDRESS;
        private System.Windows.Forms.TextBox TBEMAIL;
        private System.Windows.Forms.TextBox TBNATNUB;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ComboBox CB_GENDER;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.ComboBox CB_COUNTRY;
        private System.Windows.Forms.TextBox TBPHONE;
        private System.Windows.Forms.LinkLabel linklblSetImg;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel linklbl_Remove_Img;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label Lbl_Add_Edit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
