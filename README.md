# Background About The Application

An organization XYZ has a lot of legacy systems that manage and use customer contact information however with modernization of system in mind they are breaking up the monolithic applications and either redeveloping them or wrapping them in extensible and scalable microservices. The customer contact information is identified as one of these services.

### The Application consists of essentially 3 parts:

1. The social security number (10-12 digits)
2. A valid email address or NULL
3. A valid phone number or NULL (an optional +46 as county code followed by an 9-digit telephone number)

The Application is a REST API providing methods for retrieval, creation, update and deletion of customer information records.

## Prerequisites

##### - .NET Core 3.1

##### - Database, contains table CustomerDB

```sql
CREATE TABLE CustomerDB ( Pnr varchar(255) NOT NULL, Email varchar(255) , PhoneNumber varchar(255), PRIMARY KEY (Pnr) );
```

## Git Repository

[Repo](https://github.com/Mohamadyse/Customer.Contact.Info.Service.git)
