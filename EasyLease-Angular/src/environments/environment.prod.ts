export const environment = {
  production: true,
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
  maxArea: 10000,
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
