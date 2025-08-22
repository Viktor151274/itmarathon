# marathon2025
Creates a VPC based on variables specified either in default values of the module var file or the tfvars file of the root module, i.e.:
```
  subnet0 = {
    cidr_block = "10.0.1.0/24"
    public     = true
  },
  subnet1 = {
    cidr_block = "10.0.2.0/24"
    public     = true
  }
  
```
"public" option specifies whether a subnet is public or not.

An example output:

```
subnet_ids = {
  "subnet0" = "subnet-032ae940391017bed"
  "subnet1" = "subnet-0087baa0641c34171"
  "subnet2" = "subnet-01a9a44de5643ce7d"
  "subnet3" = "subnet-0188dfa28df0628c7"
}
vpc_cidr_block = "10.0.0.0/16"
vpc_id = "vpc-08c37bff8bdc5e6b1"
```