<template>
  <div class="table-container">
    <div class="table-wrapper">
      <h2 class="title">Submission List</h2>

      <!-- Search input field -->
      <div class="search-container">
        <input
          v-model="searchQuery"
          type="text"
          placeholder="Search by email, name, or city..."
          class="search-input"
        />
      </div>

      <div class="overflow-x-auto">
        <table class="styled-table">
          <thead>
            <tr>
              <th>Email</th>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Role</th>
              <th>City</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="submission in filteredSubmissions" :key="submission.id">
              <td>{{ getFieldValue(submission, 'email') }}</td>
              <td>{{ getFieldValue(submission, 'firstName') }}</td>
              <td>{{ getFieldValue(submission, 'lastName') }}</td>
              <td>{{ getFieldValue(submission, 'role') }}</td>
              <td>{{ getFieldValue(submission, 'city') }}</td>
              <td>
                <button @click="deleteSubmission(submission.id)" class="delete-button">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import { SubmissionService } from "@/services/api.js";

export default {
  data() {
    return {
      submissions: [],
      searchQuery: "",  // Store the search input value
    };
  },
  async created() {
    await this.fetchSubmissions();
  },
  methods: {
    async fetchSubmissions() {
      try {
        const response = await SubmissionService.getAll();
        this.submissions = response.data;
      } catch (error) {
        console.error("Error fetching submissions:", error);
      }
    },
    async deleteSubmission(id) {
      try {
        await SubmissionService.delete(id);
        this.fetchSubmissions();
      } catch (error) {
        console.error("Error deleting submission:", error);
      }
    },
    getFieldValue(submission, fieldName) {
      const field = submission.fields.find(f => f.fieldName === fieldName);
      return field ? field.value || "N/A" : "N/A";
    },
  },
  computed: {
    // Computed property to filter the submissions based on the search query
    filteredSubmissions() {
      const query = this.searchQuery.toLowerCase();
      return this.submissions.filter(submission => {
        const email = this.getFieldValue(submission, 'email').toLowerCase();
        const firstName = this.getFieldValue(submission, 'firstName').toLowerCase();
        const lastName = this.getFieldValue(submission, 'lastName').toLowerCase();
        const city = this.getFieldValue(submission, 'city').toLowerCase();
        return (
          email.includes(query) ||
          firstName.includes(query) ||
          lastName.includes(query) ||
          city.includes(query)
        );
      });
    },
  },
};
</script>

<style scoped>
.table-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background: linear-gradient(#d0d6d9, #b1b4ba);
}

.table-wrapper {
  background: linear-gradient(180deg, #253c3c, #1d4949);
  padding: 2rem;
  border-radius: 12px;
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
  width: fit-content;
}

.title {
  text-align: center;
  color: #253c3c;
  font-size: 1.5rem;
  margin-bottom: 1rem;
}

.styled-table {
  width: 100%;
  border-collapse: collapse;
  border-radius: 8px;
  overflow: hidden;
}

.styled-table th, .styled-table td {
  padding: 12px;
  text-align: left;
}

.styled-table thead {
  background-color: #338f6d;
  color: white;
}

.styled-table tr:nth-child(even) {
  background-color: #446955;
}

.delete-button {
  background-color: #ff4d4d;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: pointer;
  transition: background 0.3s;
}

.delete-button:hover {
  background-color: #e60000;
}

/* Styling for search input */
.search-container {
  margin-bottom: 1rem;
  text-align: center;
}

.search-input {
  width: 300px;
  padding: 8px;
  font-size: 1rem;
  border-radius: 4px;
  border: 1px solid #ddd;
}
</style>
