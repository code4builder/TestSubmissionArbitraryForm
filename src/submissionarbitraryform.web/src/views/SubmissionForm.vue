<template>
  <form @submit.prevent="handleSubmit">
    <h2>Welcome on board!</h2>
    <p>We just need a little bit of data from you to get you started 🚀</p>

    <!-- Email -->
    <div class="control">
      <label for="email">Email</label>
      <input v-model="form.email" id="email" type="email" name="email" />
    </div>

    <!-- Password and Confirm Password -->
    <div class="control-row">
      <div class="control">
        <label for="password">Password</label>
        <input v-model="form.password" id="password" type="password" name="password" />
      </div>

      <div class="control">
        <label for="confirm-password">Confirm Password</label>
        <input v-model="form.confirmPassword" id="confirm-password" type="password" name="confirm-password" />
      </div>
    </div>

    <hr />

    <!-- First and Last Name -->
    <div>
      <div class="control-row">
        <div class="control">
          <label for="first-name">First Name</label>
          <input v-model="form.firstName" type="text" id="first-name" name="first-name" />
        </div>

        <div class="control">
          <label for="last-name">Last Name</label>
          <input v-model="form.lastName" type="text" id="last-name" name="last-name" />
        </div>
      </div>

      <fieldset>
        <legend>Your Address</legend>

        <!-- Address fields -->
        <div class="control-row">
          <div class="control">
            <label for="street">Street</label>
            <input v-model="form.street" type="text" id="street" name="street" />
          </div>

          <div class="control">
            <label for="number">Number</label>
            <input v-model="form.number" type="text" id="number" name="number" />
          </div>
        </div>

        <div class="control-row">
          <div class="control">
            <label for="postal-code">Postal Code</label>
            <input v-model="form.postalCode" type="text" id="postal-code" name="postal-code" />
          </div>

          <div class="control">
            <label for="city">City</label>
            <input v-model="form.city" type="text" id="city" name="city" />
          </div>
        </div>
      </fieldset>
    </div>

    <hr />

    <!-- Role -->
    <div class="control-row">
      <div class="control">
        <label for="role">What best describes your role?</label>
        <select v-model="form.role" id="role" name="role">
          <option value="student">Student</option>
          <option value="teacher">Teacher</option>
          <option value="employee">Employee</option>
          <option value="founder">Founder</option>
          <option value="other">Other</option>
        </select>
      </div>
    </div>

    <fieldset>
      <legend>How did you find us?</legend>
      <!-- Acquisition options -->
      <div class="control">
        <input v-model="form.acquisition" type="checkbox" id="google" name="acquisition" value="google" />
        <label for="google">Google</label>
      </div>

      <div class="control">
        <input v-model="form.acquisition" type="checkbox" id="friend" name="acquisition" value="friend" />
        <label for="friend">Referred by friend</label>
      </div>

      <div class="control">
        <input v-model="form.acquisition" type="checkbox" id="other" name="acquisition" value="other" />
        <label for="other">Other</label>
      </div>
    </fieldset>

    <!-- Terms and Conditions -->
    <div class="control-row">
      <div class="control">
        <label for="terms-and-conditions">
          <input v-model="form.terms" type="checkbox" id="terms-and-conditions" name="terms" />
          I agree to the terms and conditions
        </label>
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
    };
  },
  methods: {
    async handleSubmit() {
      try {
        // Call the API to save the form data
        await SubmissionService.create(this.form);
        // Redirect to the Submission List view
        this.$router.push({ name: "submission-list" });
      } catch (error) {
        console.error("Error during submission:", error);
      }
    },
  },
};
</script>
