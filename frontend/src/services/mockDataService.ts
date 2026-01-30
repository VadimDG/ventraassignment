export interface SubSkuItem {
  id: number
  name: string
}

export interface SubSkuBlock {
  id: number
  name: string
  unitsH0: number
  unitsY1: number
  unitsContributionGrowth: number
  priceH0: number
  priceY1: number
  priceContributionGrowth: number
  amountH0: number
  amountY1: number
  amountContributionGrowth: number
}

export interface Payload {
  skus: Sku[]
  totals: SubSkuBlock
}

export interface Sku {
  id: number
  name: string
  subSkus: SubSkuBlock[]
  skuSum: SubSkuBlock
}

export interface MockData {
  data: Payload
}

export async function getTableData(selectedSubSkuIds?: number[]): Promise<MockData> {
  try {
    const url = new URL('https://localhost:7159/api/Planner')
    if (selectedSubSkuIds && selectedSubSkuIds.length > 0) {
      selectedSubSkuIds.forEach((id) => {
        url.searchParams.append('selectedSubSkus', id.toString())
      })
    }
    const response = await fetch(url.toString())
    if (!response.ok) {
      throw new Error(`API error: ${response.statusText}`)
    }
    const data = await response.json()
    return data
  } catch (error) {
    console.error('Failed to fetch data from API:', error)
    throw error
  }
}

export async function getSubSkuNames(): Promise<SubSkuItem[]> {
  try {
    const response = await fetch('https://localhost:7159/api/itemslist/subskunames')
    if (!response.ok) {
      throw new Error(`API error: ${response.statusText}`)
    }
    const data = await response.json()
    return data
  } catch (error) {
    console.error('Failed to fetch sub SKU names from API:', error)
    throw error
  }
}
