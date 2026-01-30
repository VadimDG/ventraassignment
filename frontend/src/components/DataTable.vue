<template>
  <div class="filter-section">
    <label for="subsku-select">Filter by Sub SKU:</label>
    <select id="subsku-select" v-model.number="selectedSubSkus" multiple class="subsku-dropdown">
      <option v-for="subsku in allSubSkusComputed" :key="subsku.id" :value="subsku.id">
        {{ subsku.name }}
      </option>
    </select>
    <button @click="handleFilterClick" class="filter-button" title="Filter">🔄</button>
    <button @click="toggleEditMode" class="edit-button" :title="isEditMode ? 'Save' : 'Edit'">
      {{ isEditMode ? '💾' : '✏️' }}
    </button>
  </div>
  <div class="table-container">
    <table class="data-table">
      <thead>
        <tr>
          <th>SKU</th>
          <th>Sub SKU</th>
          <th>Тип значения</th>
          <th>History Y0</th>
          <th>Planning Y1</th>
          <th>Contribution Growth</th>
        </tr>
      </thead>
      <tbody>
        <DataRow
          v-for="row in filteredTableData"
          :key="row.id"
          :name="row.name"
          :sku="row"
          :isEditMode="isEditMode"
        ></DataRow>
        <SumRow v-if="totals" :sum="totals" />
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { computed, ref, onMounted } from 'vue'
import {
  getTableData,
  getSubSkuNames,
  type Sku,
  type SubSkuBlock,
  type SubSkuItem,
} from '@/services/mockDataService'
import DataRow from './DataRow.vue'
import SumRow from './SumRow.vue'

const tableData = ref<Sku[]>([])
const totals = ref<SubSkuBlock>()
const allSubSkus = ref<SubSkuItem[]>([])
const selectedSubSkus = ref<number[]>([])
const isEditMode = ref(false)

onMounted(async () => {
  try {
    const [tableDataRes, subSkuNamesRes] = await Promise.all([getTableData(), getSubSkuNames()])
    totals.value = tableDataRes.data.totals
    tableData.value = tableDataRes.data.skus
    allSubSkus.value = subSkuNamesRes
  } catch (error) {
    console.error('Failed to load data:', error)
  }
})

const allSubSkusComputed = computed(() => {
  return allSubSkus.value
})

const filteredTableData = computed(() => {
  if (selectedSubSkus.value.length === 0) {
    return tableData.value
  }
  return tableData.value
    .map((row) => ({
      ...row,
      subSku: row.subSkus.filter((sub) => selectedSubSkus.value.includes(sub.id)),
    }))
    .filter((row) => row.subSku.length > 0)
})

const handleFilterClick = async () => {
  try {
    const { data } = await getTableData(
      selectedSubSkus.value.length > 0 ? selectedSubSkus.value : undefined,
    )
    tableData.value = data.skus
  } catch (error) {
    console.error('Failed to load filtered data:', error)
  }
}

const toggleEditMode = async () => {
  if (isEditMode.value) {
    await handleSave()
  }
  isEditMode.value = !isEditMode.value
}

const handleSave = async () => {
  try {
    const planningData = tableData.value.flatMap((row) =>
      row.subSkus.map((sub) => ({
        id: sub.id,
        unit: sub.unitsY1,
        price: sub.priceY1,
      })),
    )

    const response = await fetch('https://localhost:7159/api/planner', {
      method: 'PATCH',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(planningData),
    })

    if (!response.ok) {
      throw new Error(`API error: ${response.statusText}`)
    }

    console.log('Data saved successfully')
  } catch (error) {
    console.error('Failed to save data:', error)
  }
}
</script>

<style scoped>
.table-container {
  padding: 20px;
  overflow-x: auto;
}

.filter-section {
  margin-bottom: 20px;
  display: flex;
  align-items: center;
  gap: 10px;
}

.filter-section label {
  font-weight: 600;
  color: #333;
}

.subsku-dropdown {
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
  background-color: white;
  cursor: pointer;
  min-width: 200px;
  height: 120px;
}

.subsku-dropdown:hover {
  border-color: #999;
}

.subsku-dropdown:focus {
  outline: none;
  border-color: #4a90e2;
  box-shadow: 0 0 0 3px rgba(74, 144, 226, 0.1);
}

.filter-button {
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  background-color: white;
  cursor: pointer;
  font-size: 16px;
  transition: all 0.2s ease;
}

.filter-button:hover {
  border-color: #4a90e2;
  background-color: #f0f7ff;
}

.filter-button:active {
  transform: scale(0.95);
}

.edit-button {
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  background-color: white;
  cursor: pointer;
  font-size: 16px;
  transition: all 0.2s ease;
}

.edit-button:hover {
  border-color: #4a90e2;
  background-color: #f0f7ff;
}

.edit-button:active {
  transform: scale(0.95);
}

.data-table {
  width: 100%;
  border-collapse: collapse;
  font-family:
    -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}

.data-table thead {
  background-color: #f5f5f5;
}

.data-table th {
  padding: 12px;
  text-align: left;
  font-weight: 600;
  color: #333;
  border-bottom: 2px solid #ddd;
}

.data-table td {
  padding: 12px;
  border-bottom: 1px solid #eee;
}

.data-row:hover {
  background-color: #f9f9f9;
}

.total-row {
  background-color: #f0f0f0;
  font-weight: 600;
  border-top: 2px solid #ddd;
}

.total-row td {
  padding: 12px;
  border-bottom: 2px solid #ddd;
}

.number {
  text-align: right;
  font-family: 'Courier New', monospace;
}
</style>
