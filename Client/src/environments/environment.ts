// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
 production:false,
 formulas:"http://localhost:5118/api/formulas",
 formula:"http://localhost:5118/api/formulas",
 calcular:"http://localhost:5118/api/formulas/calculate",
 shipments:"http://localhost:5118/api/shipments",
 products:"http://localhost:5118/api/products",
 dashBoard:"http://localhost:5118/api/dashBoard"
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
