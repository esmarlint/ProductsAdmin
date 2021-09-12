export interface APIResponse<T> {
    payload: T[];
    ok: boolean;
    pagination: Pagination;
}

export interface Pagination {
    page: number;
    pageSize: number;
    total: number;
}

export class Product {
    id: number;
    name: string;
    description: string;
    status: string;
    statusId: number;
    prices: Price[];

    defaultPrice(): Price {
        return this.prices.find((e) => e.isDefaultPrice == true);
    }
}

export interface Price {
    id: number;
    productId: number;
    price: number;
    status: string;
    statusId: number;
    isDefaultPrice: boolean;
    colorValue: string;
    colorName: string;
    colorFormat: string;
}
