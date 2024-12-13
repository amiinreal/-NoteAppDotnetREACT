// src/components/Login.js
import React, { useState } from "react"
import axios from "../api/axios"

const Login = ({ setAuth }) => {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState("")

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      const response = await axios.post("/account/login", {
        email,
        password,
      })
      console.log(response.data) // Log the response data to the console
      if (response.data.success) {
        setAuth(true)
      } else {
        setError(response.data.errors.join(", "))
      }
    } catch (error) {
      setError("An error occurred during login.")
    }
  }

  return (
    <div>
      <h1>Login</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Email</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Password</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        {error && <p style={{ color: "red" }}>{error}</p>}
        <button type="submit">Login</button>
      </form>
    </div>
  )
}

export default Login
