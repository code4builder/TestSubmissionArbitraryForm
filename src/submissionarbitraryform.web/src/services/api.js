import axios from "axios";

const API_URL = "https://localhost:7001/api/submission";

export const SubmissionService = {
  async getAll() {
    return axios.get(API_URL);
  },
  async get(id) {
    return axios.get(`${API_URL}/${id}`);
  },
  async create(data) {
    return axios.post(API_URL, data);
  },
  async update(id, data) {
    return axios.put(`${API_URL}/${id}`, data);
  },
  async delete(id) {
    return axios.delete(`${API_URL}/${id}`);
  },
};
