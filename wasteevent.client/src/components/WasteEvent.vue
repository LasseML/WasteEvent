<template>
  <v-data-table-virtual
    :headers="headers"
    :items="wasteEvents"
    height="400"
    item-value="name"
    fixed-header
  ></v-data-table-virtual>
</template>

<script>
  import { useSignalR } from "@dreamonkey/vue-signalr";
  import { inject } from "vue";

  export default {
    setup() {
      const signalr = useSignalR();

      signalr.on("WasteEventUpdated", (wasteEvent) => {
        console.log("Received waste event update:", wasteEvent);
      });
    },
  };
</script>
