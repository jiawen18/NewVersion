using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewVersion.css
{
    public partial class Review : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Initialize a variable to collect status messages
            string statusMessage = "";

            // Check if an image file was uploaded
            HttpPostedFile imageFile = Request.Files["FileUploadImage"];
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                try
                {
                    // Define the path to save the image
                    string imagePath = Server.MapPath("~/Uploads/" + System.IO.Path.GetFileName(imageFile.FileName));

                    // Save the image file to the server
                    imageFile.SaveAs(imagePath);

                    // Add success message for image upload
                    statusMessage += "Image uploaded successfully. ";
                }
                catch (Exception ex)
                {
                    // Add error message for image upload
                    statusMessage += "Image upload failed: " + ex.Message + ". ";
                }
            }
            else
            {
                // Add message if no image file was selected
                statusMessage += "No image file selected. ";
            }

            // Check if a video file was uploaded
            HttpPostedFile videoFile = Request.Files["FileUploadVideo"];
            if (videoFile != null && videoFile.ContentLength > 0)
            {
                try
                {
                    // Define the path to save the video
                    string videoPath = Server.MapPath("~/Uploads/" + System.IO.Path.GetFileName(videoFile.FileName));

                    // Save the video file to the server
                    videoFile.SaveAs(videoPath);

                    // Add success message for video upload
                    statusMessage += "Video uploaded successfully. ";
                }
                catch (Exception ex)
                {
                    // Add error message for video upload
                    statusMessage += "Video upload failed: " + ex.Message + ". ";
                }
            }
            else
            {
                // Add message if no video file was selected
                statusMessage += "No video file selected. ";
            }

            // Display all status messages
            Label1.Text = statusMessage.Trim();


        }

    }
}
