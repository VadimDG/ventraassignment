<template>
  <tr class="data-row">
    <td class="sku-name">TOTAL</td>
    <td class="sub-sku-name">ВСЕГО</td>
    <td>
      <p>UNITS</p>
      <p>PRICE</p>
      <p>AMOUNT</p>
    </td>
    <td>
      <p class="number">{{ formatNumber(sum.unitsH0) }}</p>
      <p class="number">{{ formatNumber(sum.priceH0) }}</p>
      <p class="number">{{ formatNumber(sum.amountH0) }}</p>
    </td>
    <td>
      <p class="number">{{ formatNumber(sum.unitsY1) }}</p>
      <p class="number">{{ formatNumber(sum.priceY1) }}</p>
      <p class="number">{{ formatNumber(sum.amountY1) }}</p>
    </td>
    <td>
      <p class="number">{{ formatNumber(sum.unitsContributionGrowth) }}</p>
      <p class="number">{{ formatNumber(sum.priceContributionGrowth) }}</p>
      <p class="number">{{ formatNumber(sum.amountContributionGrowth) }}</p>
    </td>
  </tr>
</template>

<script setup lang="ts">
import type { SubSkuBlock } from '@/services/mockDataService'
import type { PropType } from 'vue'

defineProps({
  sum: {
    type: Object as PropType<SubSkuBlock>,
    required: true,
  },
})

const formatNumber = (num: number): string => {
  return new Intl.NumberFormat('en-US').format(num)
}
</script>

<style scoped>
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

.data-table {
  td.sku-name {
    vertical-align: baseline;
    padding-top: 30px;
  }

  td.sub-sku-name {
    vertical-align: baseline;
    padding-top: 30px;
  }
}

.edit-input {
  width: 80px;
  padding: 4px;
  border: 1px solid #4a90e2;
  border-radius: 3px;
  font-family: 'Courier New', monospace;
  text-align: right;
}

.edit-input:focus {
  outline: none;
  box-shadow: 0 0 0 2px rgba(74, 144, 226, 0.2);
}
</style>
