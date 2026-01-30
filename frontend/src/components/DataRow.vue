<template>
  <tr v-for="(row, index) in sku.subSkus" :key="(row as SubSkuBlock).id" class="data-row">
    <td class="sku-name" v-if="index == 0" :rowspan="sku.subSkus.length">{{ name }}</td>
    <td class="sub-sku-name">{{ (row as SubSkuBlock).name }}</td>
    <td>
      <p>UNITS</p>
      <p>PRICE</p>
      <p>AMOUNT</p>
    </td>
    <td>
      <p class="number">{{ formatNumber((row as SubSkuBlock).unitsH0) }}</p>
      <p class="number">{{ formatNumber((row as SubSkuBlock).priceH0) }}</p>
      <p class="number">{{ formatNumber((row as SubSkuBlock).amountH0) }}</p>
    </td>
    <td>
      <p class="number" v-if="!isEditMode">{{ formatNumber(row.unitsY1) }}</p>
      <p class="number" v-else>
        <input v-model.number="row.unitsY1" type="number" class="edit-input" />
      </p>
      <p class="number" v-if="!isEditMode">{{ formatNumber(row.priceY1) }}</p>
      <p class="number" v-else>
        <input v-model.number="row.priceY1" type="number" class="edit-input" />
      </p>
      <p class="number">{{ formatNumber(row.amountY1) }}</p>
    </td>
    <td>
      <p class="number">{{ formatNumber((row as SubSkuBlock).unitsContributionGrowth) }}</p>
      <p class="number">{{ formatNumber((row as SubSkuBlock).priceContributionGrowth) }}</p>
      <p class="number">{{ formatNumber((row as SubSkuBlock).amountContributionGrowth) }}</p>
    </td>
  </tr>
  <tr class="data-row">
    <td></td>
    <td class="sub-sku-name">Итого SKU</td>
    <td>
      <p>UNITS</p>
      <p>PRICE</p>
      <p>AMOUNT</p>
    </td>
    <td>
      <p class="number">{{ formatNumber(sku.skuSum.unitsH0) }}</p>
      <p class="number">{{ formatNumber(sku.skuSum.priceH0) }}</p>
      <p class="number">{{ formatNumber(sku.skuSum.amountH0) }}</p>
    </td>
    <td>
      <p class="number">{{ formatNumber(sku.skuSum.unitsY1) }}</p>
      <p class="number">{{ formatNumber((sku.skuSum as SubSkuBlock).priceY1) }}</p>
      <p class="number" v-if="!isEditMode">
        {{ formatNumber((sku.skuSum as SubSkuBlock).amountY1) }}
      </p>
    </td>
    <td>
      <p class="number">{{ formatNumber(sku.skuSum.unitsContributionGrowth) }}</p>
      <p class="number">{{ formatNumber(sku.skuSum.priceContributionGrowth) }}</p>
      <p class="number">{{ formatNumber(sku.skuSum.amountContributionGrowth) }}</p>
    </td>
  </tr>
</template>

<script setup lang="ts">
import type { Sku, SubSkuBlock } from '@/services/mockDataService'
import type { PropType } from 'vue'

defineProps({
  sku: {
    type: Object as PropType<Sku>,
    required: true,
  },
  name: {
    type: String,
    required: true,
  },
  isEditMode: {
    type: Boolean,
    required: false,
    default: false,
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
