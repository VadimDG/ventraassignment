<script setup lang="ts">
import DataTable from '@/components/DataTable.vue'
</script>

<template>
  <div id="app">
    <header class="app-header">
      <h1>Ventra - Product Planning Table</h1>
    </header>
    <main>
      <DataTable />
    </main>
  </div>
</template>

<style scoped>
#app {
  min-height: 100vh;
  background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
}

.app-header {
  padding: 20px;
  background-color: #fff;
  border-bottom: 1px solid #ddd;
}

.app-header h1 {
  margin: 0;
  color: #333;
  font-size: 28px;
}

main {
  padding: 20px;
}
</style>
