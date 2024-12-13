import React from "react"
import userImg from "../assets/image.png"

function Comment({ comments }) {
  return (
    <div>
      <h5 className="comments-header" aria-label="Comments section header">
        Comments
      </h5>
      <div className="discussions">
        <div
          className="comments-scrollable"
          aria-label="Scrollable comments list"
        >
          {comments.map((comment) => (
            <div key={comment.id}>
              <div className="discussion mb-3"> </div>
              <div className="d-flex justify-content-between align-items-center">
                <div className="d-flex align-items-center">
                  {/* User Info */}
                  <div className="userImg d-flex align-items-center">
                    <a href="/" className="nav-link text-black">
                      <img
                        src={userImg}
                        alt="Avatar"
                        style={{ width: "25px", height: "25px" }}
                      />
                      <span className="username">{comment.username}</span>
                    </a>
                  </div>
                  <div className="comment-date">
                    <small>
                      {new Date(comment.createdAt).toLocaleDateString()}
                    </small>
                  </div>
                </div>
              </div>
              <div className="comment">
                <p>{comment.content}</p>
              </div>
            </div>
          ))}
        </div>
      </div>
      <form
        asp-action="AddComment"
        method="post"
        className="comment-form mt-3"
        aria-label="Add comment form"
      >
        <input type="hidden" name="postId" value="@Model.Id" />
        <textarea
          className="form-control"
          name="Content"
          placeholder="Add a comment"
          required
        ></textarea>
        <button
          type="submit"
          className="submitcomment btn btn-primary mt-3"
          aria-label="Submit comment"
        >
          Comment
        </button>
      </form>
    </div>
  )
}

export default Comment
