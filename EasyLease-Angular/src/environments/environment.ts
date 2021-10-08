// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  originUrl: 'https://localhost:4200',
  apiUrl: 'https://localhost:5001/api',
  uploadUrl: 'https://localhost:5001',
  limit: 10,

  hoursOffsetForUkraine: 3,
  additionalTimeOnExpenses: 1,

  minUserName: 1,
  maxUserName: 10,
  minUserPassword: 8,

  maxTitle: 150,
  maxDescription: 1000,
  minArea: 1,
  maxArea: 100000,
  minNumberOfRooms: 1,
  maxNumberOfRooms: 500,
  minNumberOfStoreys: 1,
  maxNumberOfStoreys: 1000,
  minStorey: 1,
  maxStorey: 1000,
  minSettlementName: 1,
  maxSettlementName: 100,
  minStreetName: 1,
  maxStreetName: 150,
  maxHouseNumber: 50,
  maxApartmentNumber: 10000,
  minPrice: 0,
  maxPrice: 1000000,
  maxTag: 30,
  tagListLimit: 5,

  fileSizeLimit: 2097152,
  numberOfFilesLimit: 6,
  allowedExtensions: ['.jpg', '.jpeg', '.png', '.gif'],
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error',  // Included with Angular CLI.
