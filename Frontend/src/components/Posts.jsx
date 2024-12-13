// src/components/Posts.js
import React, { useEffect, useState } from "react"
import axios from "../api/axios"
import userImg from "../assets/image.png"
import Comment from "./Comment"
import CreatePost from "./CreatePost"

const Posts = () => {
  const [posts, setPosts] = useState([])
  const [error, setError] = useState("")

  const fetchPosts = async () => {
    try {
      const response = await axios.get("/post")
      if (response.data && response.data.$values) {
        const transformedPosts = response.data.$values.map((post) => ({
          ...post,
          comments: post.comments.$values,
        }))
        // Sort posts by creation date in descending order
        transformedPosts.sort(
          (a, b) => new Date(b.createdAt) - new Date(a.createdAt)
        )
        setPosts(transformedPosts)
      } else {
        setError("Unexpected response format")
      }
    } catch (error) {
      setError("An error occurred while fetching posts.")
    }
  }

  useEffect(() => {
    fetchPosts()
  }, [])

  return (
    <div className="main-content mt-5">
      <div className="content-inner">
        <CreatePost onPostCreated={fetchPosts} />
        <h1 className="posts-header">LATEST POSTS</h1>
        {error && <div className="alert alert-danger">{error}</div>}
        {posts.map((post) => (
          <div className="post" key={post.id}>
            <div className="left-content">
              {post.imageData ? (
                <div className="imagecontent">
                  <img
                    src={`data:image/jpeg;base64,${post.imageData}`}
                    alt="Post"
                    aria-label="Post image"
                  />
                </div>
              ) : (
                <div className="notecontentwithoutimage">
                  <p aria-label="Post content">{post.content}</p>
                </div>
              )}
            </div>
            <div className="right-content">
              <div
                className="post-details-box"
                aria-label="Post details and comments"
              >
                <div className="post-header">
                  <div className="userImg">
                    <img src={userImg} alt="User profile" />
                  </div>
                  <a
                    className="nav-link text-black"
                    href="/"
                    aria-label="User profile"
                  >
                    <span className="username" aria-label="Username">
                      {post.username}
                    </span>
                  </a>
                  <div className="post-date" aria-label="Post date">
                    {new Date(post.createdAt).toLocaleDateString()}
                  </div>
                </div>
                {post.imageData ? (
                  <div className="noteContentWithImage">
                    <p>{post.content}</p>
                  </div>
                ) : (
                  <div className=""></div>
                )}
              </div>
              {/* Comments section */}
              <Comment comments={post.comments} />
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

export default Posts
