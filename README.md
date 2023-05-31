# rws.authentication

.net core 6 api with an auth endpoint that receives by post on the request body the userName and password and validate this credentials against the values registered into the database (InMemory) and if its valid return a token that allow the users list the personal info in another react app;

Deployment instructions:

Please deploy the api on 'http://localhost:8080/'


The react apps are in the folder FrontEnd

Deploy the PersonalInfoApp on port 'http://localhost:8000/'

The loginApp can be deployed on the port of your preference.

Manual test:

Run the login app on the browser enter the credential that are on the page and if it is ok you will be redirected to the personalInfoAPP otherwise the error code is showed.

