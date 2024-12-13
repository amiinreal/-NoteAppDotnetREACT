// src/api/axios.js
import axios from "axios"

const instance = axios.create({
  baseURL: "http://localhost:5264/api", // Adjust the base URL as needed
  headers: {
    "Content-Type": "application/json",
  },
})

export default instance
