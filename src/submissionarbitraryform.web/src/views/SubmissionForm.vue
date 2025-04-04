<template>
  <form @submit.prevent="handleSubmit">
    <h2>Welcome on board!</h2>
    <p>We just need a little bit of data from you to get you started 🚀</p>

    <!-- Email -->
    <div class="control">
      <label for="email">Email</label>
      <input v-model="form.email" id="email" type="email" name="email" />
      <span class="error" v-if="errors.email">{{ errors.email }}</span>
    </div>

    <!-- Password and Confirm Password -->
    <div class="control-row">
      <div class="control">
        <label for="password">Password</label>
        <input v-model="form.password" id="password" type="password" name="password" />
        <span class="error" v-if="errors.password">{{ errors.password }}</span>
      </div>

      <div class="control">
        <label for="confirm-password">Confirm Password</label>
        <input v-model="form.confirmPassword" id="confirm-password" type="password" name="confirm-password" />
        <span class="error" v-if="errors.confirmPassword">{{ errors.confirmPassword }}</span>
      </div>
    </div>

    <hr />

    <!-- First and Last Name -->
    <div>
      <div class="control-row">
        <div class="control">
          <label for="first-name">First Name</label>
          <input v-model="form.firstName" type="text" id="first-name" name="first-name" />
          <span class="error" v-if="errors.firstName">{{ errors.firstName }}</span>
        </div>

        <div class="control">
          <label for="last-name">Last Name</label>
          <input v-model="form.lastName" type="text" id="last-name" name="last-name" />
          <span class="error" v-if="errors.lastName">{{ errors.lastName }}</span>
        </div>
      </div>
    </div>

    <hr />

    <!-- Role -->
    <div class="control">
      <label for="role">What best describes your role?</label>
      <select v-model="form.role" id="role" name="role">
        <option value="">Select a role</option>
        <option value="student">Student</option>
        <option value="teacher">Teacher</option>
        <option value="employee">Employee</option>
        <option value="founder">Founder</option>
        <option value="other">Other</option>
      </select>
      <span class="error" v-if="errors.role">{{ errors.role }}</span>
    </div>

    <hr />

    <!-- Terms and Conditions -->
    <div class="control-row">
      <div class="control">
        <label for="terms-and-conditions">
          <input v-model="form.terms" type="checkbox" id="terms-and-conditions" name="terms" />
          I agree to the terms and conditions
        </label>
        <span class="error" v-if="errors.terms">{{ errors.terms }}</span>
      </div>
    </div>

    <p class="form-actions">
      <button type="reset" class="button button-flat">Reset</button>
      <button type="submit" class="button">Sign up</button>
    </p>
  </form>
</template>

<script>
import { SubmissionService } from "@/services/api.js";

export default {
  data() {
    return {
      form: {
        email: "",
        password: "",
        confirmPassword: "",
        firstName: "",
        lastName: "",
        street: "",
        number: "",
        postalCode: "",
        city: "",
        role: "",
        acquisition: [],
        terms: false,
      },
      errors: {},
    };
  },
  methods: {
    validateForm() {
      this.errors = {};

      if (!this.form.email) {
        this.errors.email = "Email is required.";
      } else if (!/\S+@\S+\.\S+/.test(this.form.email)) {
        this.errors.email = "Invalid email format.";
      }

      if (!this.form.password) {
        this.errors.password = "Password is required.";
      } else if (this.form.password.length < 6) {
        this.errors.password = "Password must be at least 6 characters.";
      }

      if (this.form.confirmPassword !== this.form.password) {
        this.errors.confirmPassword = "Passwords do not match.";
      }

      if (!this.form.firstName) {
        this.errors.firstName = "First Name is required.";
      }

      if (!this.form.lastName) {
        this.errors.lastName = "Last Name is required.";
      }

      if (!this.form.role) {
        this.errors.role = "Please select a role.";
      }

      if (!this.form.terms) {
        this.errors.terms = "You must agree to the terms.";
      }

      return Object.keys(this.errors).length === 0;
    },
    async handleSubmit() {
      if (!this.validateForm()) return;

      try {
        await SubmissionService.create(this.form);
        this.$router.push({ name: "submission-list" });
      } catch (error) {
        console.error("Error during submission:", error);
      }
    },
  },
};
</script>

<style scoped>
.error {
  color: red;
  font-size: 0.9em;
}
</style>
