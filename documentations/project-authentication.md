# Project Authentication
Web Api 2 Token Authentication



#
### Steps

* Step One - get authentication token: 
  * Request uri - http://localhost:49272/token
  * Method type - POST
  * Headers - Content-Type: application/x-www-form-urlencoded
  * Body - { "username": "", "password": "", "grant_type": "password" }


  
* Step two - add bearer token example: 
  * Request uri - http://localhost:49272/api/directors/getall
  * Method type - GET
  * Headers - Authorization: Bearer Cq5L_M_53x4Yca1iUHL45WXyPUbaxNLxyjbEXwOALke5j6kD6-Es0YsgghgK0EwIkaj1O4R1nXLvnkJEqRdaorc48uvhPGUD-TVFit1RyegXZtaDUX4dBMNjQv1A3oPSeMQHKrGZZYwCiflsEn9Mb8vgXCSglAagTt6mJwIHBcx3xuTiwsRh4vtTCBeKhMvypylM6I27Rmcql-Bepz5hS1h37DM5D46oX220rurhAQw
