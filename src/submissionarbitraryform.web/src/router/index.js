import { createRouter, createWebHistory } from "vue-router";
import SubmissionList from "../views/SubmissionList.vue";
import SubmissionForm from "../views/SubmissionForm.vue";

const routes = [
  { path: "/", name: "submission-list", component: SubmissionList },
  { path: "/new", name: "submission-form", component: SubmissionForm },
  { path: "/edit/:id", name: "submission-edit", component: SubmissionForm, props: true },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
