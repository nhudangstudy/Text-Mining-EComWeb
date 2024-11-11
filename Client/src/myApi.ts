/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface AccessTokenModel {
  /**
   * @minLength 1
   * @pattern ^[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+\.?[A-Za-z0-9-_.+/=]*$
   */
  value: string;
  /** @format date-time */
  expire: string;
}

export interface CheckRequestAuthenticationModel {
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  /**
   * @minLength 1
   * @pattern [0-9]{6}
   */
  code: string;
}

export interface CreateBrandModel {
  brandName?: string | null;
}

export interface CreateCategoryModel {
  categoryName?: string | null;
  categoryDescription?: string | null;
}

export interface CreateProductColorModel {
  colorHex?: string | null;
}

export interface CreateProductImageModel {
  imageUrl?: string | null;
}

export interface CreateProductPriceModel {
  /** @format double */
  price?: number | null;
  /** @format double */
  discount?: number | null;
}

export interface CreateProductRequestModel {
  asin?: string | null;
  /** @format int32 */
  subCategoryId?: number;
  /** @format int32 */
  brandId?: number | null;
  ownedBy?: string | null;
  productShortDescription?: string | null;
  productDetailDescription?: string | null;
  productName?: string | null;
  productColors?: CreateProductColorModel[] | null;
  productImages?: CreateProductImageModel[] | null;
  productPriceHistories?: CreateProductPriceModel;
}

export interface CreateReviewRequest {
  title?: string | null;
  imageUrl?: string | null;
  /** @format double */
  starRating?: number;
  reviewContent?: string | null;
}

export interface CreateSubCategoryModel {
  /** @format int32 */
  categoryId?: number;
  subCategoryName?: string | null;
  subCategoryDescription?: string | null;
}

export interface CreateUpdateUserRequestModel {
  /** @minLength 1 */
  firstName: string;
  /** @minLength 1 */
  lastName: string;
  /** @format date-time */
  dateOfBirth: string;
}

export interface DateOnly {
  /** @format int32 */
  year?: number;
  /** @format int32 */
  month?: number;
  /** @format int32 */
  day?: number;
  dayOfWeek?: DayOfWeek;
  /** @format int32 */
  dayOfYear?: number;
  /** @format int32 */
  dayNumber?: number;
}

export enum DayOfWeek {
  Sunday = "sunday",
  Monday = "monday",
  Tuesday = "tuesday",
  Wednesday = "wednesday",
  Thursday = "thursday",
  Friday = "friday",
  Saturday = "saturday",
}

export interface GetAccessByRefreshRequestTokenModel {
  /** @format uuid */
  refreshToken: string;
}

export interface GetByIdPublicAccountModel {
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  /** @minLength 1 */
  fullName: string;
  isActive?: boolean | null;
}

export interface GetByLoginTokenModel {
  access: AccessTokenModel;
  refresh?: RefreshTokenModel;
}

export interface GetLastNameByIdAccountModel {
  /** @minLength 1 */
  lastName: string;
}

export interface LoginRequestAccountModel {
  /**
   * @format email
   * @minLength 1
   */
  clientId: string;
  /**
   * @format password
   * @minLength 1
   */
  password: string;
}

export interface ProductColorResponseModel {
  /** @format int32 */
  id?: number;
  colorHex?: string | null;
}

export interface ProductImageResponseModel {
  /** @format int32 */
  id?: number;
  imageUrl?: string | null;
  isArchived?: boolean;
}

export interface ProductPriceHistoryResponseModel {
  /** @format int32 */
  id?: number;
  /** @format double */
  price?: number | null;
  /** @format double */
  discount?: number | null;
  dateUpdated?: DateOnly;
}

export interface ProductResponseModel {
  asin?: string | null;
  /** @format int32 */
  subCategoryId?: number;
  /** @format int32 */
  brandId?: number | null;
  ownedBy?: string | null;
  productShortDescription?: string | null;
  productDetailDescription?: string | null;
  productName?: string | null;
  isArchived?: boolean;
  productColors?: ProductColorResponseModel[] | null;
  productImages?: ProductImageResponseModel[] | null;
  productPriceHistories?: ProductPriceHistoryResponseModel[] | null;
}

export interface RefreshTokenModel {
  /** @format uuid */
  value: string;
  /** @format date-time */
  expire: string;
}

export interface RegisterRequestAccountModel {
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  /**
   * @format password
   * @minLength 6
   * @maxLength 64
   */
  password: string;
  /**
   * @format password
   * @minLength 6
   * @maxLength 64
   */
  repeatPassword: string;
}

export interface ResetPasswordRequestAccountModel {
  auth: CheckRequestAuthenticationModel;
  /**
   * @format password
   * @minLength 6
   * @maxLength 64
   */
  newPassword: string;
}

export interface ReviewResponseModel {
  /** @format uuid */
  id?: string;
  asin?: string | null;
  title?: string | null;
  imageUrl?: string | null;
  /** @format double */
  starRating?: number;
  reviewContent?: string | null;
  ownedBy?: string | null;
  sentiment?: string | null;
}

export interface SendRequestAuthenticationModel {
  /**
   * @format email
   * @minLength 1
   */
  email: string;
}

export interface UpdateBrandModel {
  brandName?: string | null;
}

export interface UpdateCategoryModel {
  categoryName?: string | null;
  categoryDescription?: string | null;
}

export interface UpdateSubCategoryModel {
  subCategoryName?: string | null;
  subCategoryDescription?: string | null;
}

export type QueryParamsType = Record<string | number, any>;
export type ResponseFormat = keyof Omit<Body, "body" | "bodyUsed">;

export interface FullRequestParams extends Omit<RequestInit, "body"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseFormat;
  /** request body */
  body?: unknown;
  /** base url */
  baseUrl?: string;
  /** request cancellation token */
  cancelToken?: CancelToken;
}

export type RequestParams = Omit<FullRequestParams, "body" | "method" | "query" | "path">;

export interface ApiConfig<SecurityDataType = unknown> {
  baseUrl?: string;
  baseApiParams?: Omit<RequestParams, "baseUrl" | "cancelToken" | "signal">;
  securityWorker?: (securityData: SecurityDataType | null) => Promise<RequestParams | void> | RequestParams | void;
  customFetch?: typeof fetch;
}

export interface HttpResponse<D extends unknown, E extends unknown = unknown> extends Response {
  data: D;
  error: E;
}

type CancelToken = Symbol | string | number;

export enum ContentType {
  Json = "application/json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public baseUrl: string = "https://lokeytechbackend-g0d2hdfeavape2ev.canadacentral-01.azurewebsites.net";
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private abortControllers = new Map<CancelToken, AbortController>();
  private customFetch = (...fetchParams: Parameters<typeof fetch>) => fetch(...fetchParams);

  private baseApiParams: RequestParams = {
    credentials: "same-origin",
    headers: {},
    redirect: "follow",
    referrerPolicy: "no-referrer",
  };

  constructor(apiConfig: ApiConfig<SecurityDataType> = {}) {
    Object.assign(this, apiConfig);
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected encodeQueryParam(key: string, value: any) {
    const encodedKey = encodeURIComponent(key);
    return `${encodedKey}=${encodeURIComponent(typeof value === "number" ? value : `${value}`)}`;
  }

  protected addQueryParam(query: QueryParamsType, key: string) {
    return this.encodeQueryParam(key, query[key]);
  }

  protected addArrayQueryParam(query: QueryParamsType, key: string) {
    const value = query[key];
    return value.map((v: any) => this.encodeQueryParam(key, v)).join("&");
  }

  protected toQueryString(rawQuery?: QueryParamsType): string {
    const query = rawQuery || {};
    const keys = Object.keys(query).filter((key) => "undefined" !== typeof query[key]);
    return keys
      .map((key) => (Array.isArray(query[key]) ? this.addArrayQueryParam(query, key) : this.addQueryParam(query, key)))
      .join("&");
  }

  protected addQueryParams(rawQuery?: QueryParamsType): string {
    const queryString = this.toQueryString(rawQuery);
    return queryString ? `?${queryString}` : "";
  }

  private contentFormatters: Record<ContentType, (input: any) => any> = {
    [ContentType.Json]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string") ? JSON.stringify(input) : input,
    [ContentType.Text]: (input: any) => (input !== null && typeof input !== "string" ? JSON.stringify(input) : input),
    [ContentType.FormData]: (input: any) =>
      Object.keys(input || {}).reduce((formData, key) => {
        const property = input[key];
        formData.append(
          key,
          property instanceof Blob
            ? property
            : typeof property === "object" && property !== null
              ? JSON.stringify(property)
              : `${property}`,
        );
        return formData;
      }, new FormData()),
    [ContentType.UrlEncoded]: (input: any) => this.toQueryString(input),
  };

  protected mergeRequestParams(params1: RequestParams, params2?: RequestParams): RequestParams {
    return {
      ...this.baseApiParams,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...(this.baseApiParams.headers || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected createAbortSignal = (cancelToken: CancelToken): AbortSignal | undefined => {
    if (this.abortControllers.has(cancelToken)) {
      const abortController = this.abortControllers.get(cancelToken);
      if (abortController) {
        return abortController.signal;
      }
      return void 0;
    }

    const abortController = new AbortController();
    this.abortControllers.set(cancelToken, abortController);
    return abortController.signal;
  };

  public abortRequest = (cancelToken: CancelToken) => {
    const abortController = this.abortControllers.get(cancelToken);

    if (abortController) {
      abortController.abort();
      this.abortControllers.delete(cancelToken);
    }
  };

  public request = async <T = any, E = any>({
    body,
    secure,
    path,
    type,
    query,
    format,
    baseUrl,
    cancelToken,
    ...params
  }: FullRequestParams): Promise<HttpResponse<T, E>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.baseApiParams.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const queryString = query && this.toQueryString(query);
    const payloadFormatter = this.contentFormatters[type || ContentType.Json];
    const responseFormat = format || requestParams.format;

    return this.customFetch(`${baseUrl || this.baseUrl || ""}${path}${queryString ? `?${queryString}` : ""}`, {
      ...requestParams,
      headers: {
        ...(requestParams.headers || {}),
        ...(type && type !== ContentType.FormData ? { "Content-Type": type } : {}),
      },
      signal: (cancelToken ? this.createAbortSignal(cancelToken) : requestParams.signal) || null,
      body: typeof body === "undefined" || body === null ? null : payloadFormatter(body),
    }).then(async (response) => {
      const r = response.clone() as HttpResponse<T, E>;
      r.data = null as unknown as T;
      r.error = null as unknown as E;

      const data = !responseFormat
        ? r
        : await response[responseFormat]()
            .then((data) => {
              if (r.ok) {
                r.data = data;
              } else {
                r.error = data;
              }
              return r;
            })
            .catch((e) => {
              r.error = e;
              return r;
            });

      if (cancelToken) {
        this.abortControllers.delete(cancelToken);
      }

      if (!response.ok) throw data;
      return data;
    });
  };
}

/**
 * @title Quick Start
 * @version 3.0.45.156
 * @baseUrl https://lokeytechbackend-g0d2hdfeavape2ev.canadacentral-01.azurewebsites.net
 * @contact nhudangstudy, anhhoangdev, cocaa <nhucnh21416c@st.uel.edu.vn, anhth21416c@st.uel.edu.vn, hienttm21416c@st.uel.edu.vn>
 *
 * <p>Documentation API of <b>Text Mining Project</b> has been written by Live Laugh Love.</p><p><b><i>This is an internal resource. Do not share it in any way.</i></b></p><p>You should read carefully the descriptions, response status codes and arguments request belong to each API Resource. It take your time, but you will avoid a lot of bugs afterward.</p><b>The application case sensitive.</b>
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags Accounts
     * @name AccountsLastNameList
     * @request GET:/api/accounts/last-name
     * @secure
     */
    accountsLastNameList: (params: RequestParams = {}) =>
      this.request<GetLastNameByIdAccountModel, any>({
        path: `/api/accounts/last-name`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Accounts
     * @name AccountsIsExistDetail
     * @request GET:/api/accounts/is-exist/{id}
     * @secure
     */
    accountsIsExistDetail: (id: string, params: RequestParams = {}) =>
      this.request<boolean, any>({
        path: `/api/accounts/is-exist/${id}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Accounts
     * @name AccountsCreate
     * @request POST:/api/accounts
     * @secure
     */
    accountsCreate: (data: RegisterRequestAccountModel, params: RequestParams = {}) =>
      this.request<GetByIdPublicAccountModel, any>({
        path: `/api/accounts`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Accounts
     * @name AccountsActiveCreate
     * @request POST:/api/accounts/active
     * @secure
     */
    accountsActiveCreate: (data: CheckRequestAuthenticationModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/accounts/active`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Accounts
     * @name AccountsPartialUpdate
     * @request PATCH:/api/accounts/{id}
     * @secure
     */
    accountsPartialUpdate: (
      id: string,
      query?: {
        fullName?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/accounts/${id}`,
        method: "PATCH",
        query: query,
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Accounts
     * @name AccountsDelete
     * @request DELETE:/api/accounts/{id}
     * @secure
     */
    accountsDelete: (id: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/accounts/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Accounts
     * @name AccountsResetPasswordPartialUpdate
     * @request PATCH:/api/accounts/reset-password
     * @secure
     */
    accountsResetPasswordPartialUpdate: (data: ResetPasswordRequestAccountModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/accounts/reset-password`,
        method: "PATCH",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Authentications
     * @name AuthenticationsCheckCreate
     * @request POST:/api/authentications/check
     * @secure
     */
    authenticationsCheckCreate: (data: CheckRequestAuthenticationModel, params: RequestParams = {}) =>
      this.request<boolean, any>({
        path: `/api/authentications/check`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Authentications
     * @name AuthenticationsCreate
     * @request POST:/api/authentications
     * @secure
     */
    authenticationsCreate: (data: SendRequestAuthenticationModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/authentications`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Brands
     * @name BrandsDetail
     * @request GET:/api/brands/{id}
     * @secure
     */
    brandsDetail: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/brands/${id}`,
        method: "GET",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Brands
     * @name BrandsUpdate
     * @request PUT:/api/brands/{id}
     * @secure
     */
    brandsUpdate: (id: number, data: UpdateBrandModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/brands/${id}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Brands
     * @name BrandsDelete
     * @request DELETE:/api/brands/{id}
     * @secure
     */
    brandsDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/brands/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Brands
     * @name BrandsList
     * @request GET:/api/brands
     * @secure
     */
    brandsList: (
      query?: {
        /** @format int32 */
        size?: number;
        /**
         * @format int32
         * @default 1
         */
        page?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/brands`,
        method: "GET",
        query: query,
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Brands
     * @name BrandsCreate
     * @request POST:/api/brands
     * @secure
     */
    brandsCreate: (data: CreateBrandModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/brands`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesDetail
     * @request GET:/api/categories/{id}
     * @secure
     */
    categoriesDetail: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/categories/${id}`,
        method: "GET",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesUpdate
     * @request PUT:/api/categories/{id}
     * @secure
     */
    categoriesUpdate: (id: number, data: UpdateCategoryModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/categories/${id}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesDelete
     * @request DELETE:/api/categories/{id}
     * @secure
     */
    categoriesDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/categories/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesList
     * @request GET:/api/categories
     * @secure
     */
    categoriesList: (
      query?: {
        /** @format int32 */
        size?: number;
        /**
         * @format int32
         * @default 1
         */
        page?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/categories`,
        method: "GET",
        query: query,
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesCreate
     * @request POST:/api/categories
     * @secure
     */
    categoriesCreate: (data: CreateCategoryModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/categories`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Products
     * @name ProductsPublicList
     * @request GET:/api/products/public
     * @secure
     */
    productsPublicList: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/products/public`,
        method: "GET",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Products
     * @name ProductsAdminList
     * @request GET:/api/products/admin
     * @secure
     */
    productsAdminList: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/products/admin`,
        method: "GET",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Products
     * @name ProductsUpdate
     * @request PUT:/api/products/{asin}
     * @secure
     */
    productsUpdate: (asin: string, data: CreateProductRequestModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/products/${asin}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Products
     * @name ProductsDetail
     * @request GET:/api/products/{asin}
     * @secure
     */
    productsDetail: (asin: string, params: RequestParams = {}) =>
      this.request<ProductResponseModel, any>({
        path: `/api/products/${asin}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Products
     * @name ProductsList
     * @request GET:/api/products
     * @secure
     */
    productsList: (
      query?: {
        /** @format int32 */
        size?: number;
        /**
         * @format int32
         * @default 1
         */
        page?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/products`,
        method: "GET",
        query: query,
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Products
     * @name ProductsPriceUpdate
     * @request PUT:/api/products/{asin}/price
     * @secure
     */
    productsPriceUpdate: (asin: string, data: CreateProductPriceModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/products/${asin}/price`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Review
     * @name ReviewsProductDetail
     * @request GET:/api/reviews/product/{asin}
     * @secure
     */
    reviewsProductDetail: (asin: string, params: RequestParams = {}) =>
      this.request<ReviewResponseModel[], any>({
        path: `/api/reviews/product/${asin}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Review
     * @name ReviewsUserDetail
     * @request GET:/api/reviews/user/{email}
     * @secure
     */
    reviewsUserDetail: (email: string, params: RequestParams = {}) =>
      this.request<ReviewResponseModel[], any>({
        path: `/api/reviews/user/${email}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Review
     * @name ReviewsFeaturedList
     * @request GET:/api/reviews/featured
     * @secure
     */
    reviewsFeaturedList: (
      query?: {
        /**
         * @format int32
         * @default 4
         */
        count?: number;
        /**
         * @format double
         * @default 4
         */
        minimumRating?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<ReviewResponseModel[], any>({
        path: `/api/reviews/featured`,
        method: "GET",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Review
     * @name ReviewsCreate
     * @request POST:/api/reviews/{asin}
     * @secure
     */
    reviewsCreate: (asin: string, data: CreateReviewRequest, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/reviews/${asin}`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags SubCategories
     * @name SubCategoriesDetail
     * @request GET:/api/sub-categories/{id}
     * @secure
     */
    subCategoriesDetail: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/sub-categories/${id}`,
        method: "GET",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags SubCategories
     * @name SubCategoriesUpdate
     * @request PUT:/api/sub-categories/{id}
     * @secure
     */
    subCategoriesUpdate: (id: number, data: UpdateSubCategoryModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/sub-categories/${id}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags SubCategories
     * @name SubCategoriesDelete
     * @request DELETE:/api/sub-categories/{id}
     * @secure
     */
    subCategoriesDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/sub-categories/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags SubCategories
     * @name SubCategoriesList
     * @request GET:/api/sub-categories
     * @secure
     */
    subCategoriesList: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/sub-categories`,
        method: "GET",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags SubCategories
     * @name SubCategoriesCreate
     * @request POST:/api/sub-categories
     * @secure
     */
    subCategoriesCreate: (data: CreateSubCategoryModel, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/sub-categories`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),
  };
  token = {
    /**
     * No description
     *
     * @tags Token
     * @name TokenCreate
     * @request POST:/token
     * @secure
     */
    tokenCreate: (
      data: LoginRequestAccountModel,
      query?: {
        includeRefreshToken?: boolean;
      },
      params: RequestParams = {},
    ) =>
      this.request<GetByLoginTokenModel, any>({
        path: `/token`,
        method: "POST",
        query: query,
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Token
     * @name TokenList
     * @request GET:/token
     * @secure
     */
    tokenList: (params: RequestParams = {}) =>
      this.request<string[], any>({
        path: `/token`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Token
     * @name GoogleAuthCreate
     * @request POST:/token/google-auth
     * @secure
     */
    googleAuthCreate: (
      data: string,
      query?: {
        includeRefreshToken?: boolean;
      },
      params: RequestParams = {},
    ) =>
      this.request<GetByLoginTokenModel, any>({
        path: `/token/google-auth`,
        method: "POST",
        query: query,
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Token
     * @name RefreshCreate
     * @request POST:/token/refresh
     * @secure
     */
    refreshCreate: (data: GetAccessByRefreshRequestTokenModel, params: RequestParams = {}) =>
      this.request<AccessTokenModel, any>({
        path: `/token/refresh`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  };
  user = {
    /**
     * No description
     *
     * @tags User
     * @name UserCreate
     * @request POST:/user/{email}
     * @secure
     */
    userCreate: (email: string, data: CreateUpdateUserRequestModel, params: RequestParams = {}) =>
      this.request<GetByIdPublicAccountModel, any>({
        path: `/user/${email}`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name UserDetail
     * @request GET:/user/{email}
     * @secure
     */
    userDetail: (email: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/user/${email}`,
        method: "GET",
        secure: true,
        ...params,
      }),
  };
}
