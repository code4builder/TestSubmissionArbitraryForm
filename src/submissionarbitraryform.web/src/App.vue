<template>
  <div>
    <nav class="d-flex justify-content-center my-3">
      <button @click="navigateTo('/')" class="button mx-2">Home</button>
      <button @click="navigateTo('/new')" class="button">New Submission</button>
    </nav>
    <hr />
    <router-view />
  </div>
</template>

<script>
export default {
  methods: {
    navigateTo(route) {
      this.$router.push(route);
    }
  }
};
</script>