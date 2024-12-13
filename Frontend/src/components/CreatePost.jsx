import React, { useState } from "react"
import axios from "../api/axios"

function CreatePost({ onPostCreated }) {
  const [content, setContent] = useState("")
  const [image, setImage] = useState(null)
  const [username] = useState("cool") // Replace with actual username
  const [error, setError] = useState("")
  const [success, setSuccess] = useState("")

  const handleImageChange = (event) => {
    const file = event.target.files[0]
    if (file) {
      setImage(file)
      const reader = new FileReader()
      reader.onload = (e) => {
        document.getElementById("imagePreview").src = e.target.result
      }
      reader.readAsDataURL(file)
    }
  }

  const handleSubmit = async (event) => {
    event.preventDefault()
    setError("")
    setSuccess("")

    const formData = new FormData()
    formData.append("Content", content)
    formData.append("Username", username)
    if (image) {
      formData.append("Image", image)
    }

    try {
      const response = await axios.post("/post", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      if (response.data.success) {
        setSuccess("Post created successfully!")
        setContent("")
        setImage(null)
        document.getElementById("imagePreview").src = ""
        onPostCreated() // Refresh the posts list
        setTimeout(() => {
          setSuccess("")
        }, 1500)
      } else {
        setError(response.data.message)
      }
    } catch (error) {
      setError("An error occurred while creating the post.")
    }
  }

  return (
    <form onSubmit={handleSubmit} className="create-post-container d-flex">
      {/* Left side for image preview and selecting image */}
      <div className="left-column flex-grow-1">
        <img
          className="image-placeholder"
          id="imagePreview"
          alt="Selected"
          style={{ width: "100%", height: "auto", textAlign: "center" }}
        />
        <label
          htmlFor="fileUpload"
          className="custom-file-upload btn btn-primary mt-3"
        >
          Select Image
        </label>
        <input
          type="file"
          accept="image/*"
          className="form-control-file"
          name="image"
          id="fileUpload"
          style={{ display: "none" }}
          onChange={handleImageChange}
        />
      </div>

      {/* Right side for writing the note */}
      <div className="right-column flex-grow-1">
        <textarea
          name="Content"
          id="noteInput"
          className="form-control"
          placeholder="Write note here..."
          value={content}
          onChange={(e) => setContent(e.target.value)}
          required
        ></textarea>
        <button type="submit" className="postbutton btn btn-primary mt-3">
          Post
        </button>
      </div>
      {error && <div className="alert alert-danger mt-3">{error}</div>}
      {success && <div className="alert alert-success mt-3">{success}</div>}
    </form>
  )
}

export default CreatePost
