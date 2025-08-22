# Security Groups Module
This Terraform module creates security groups for a multi-tier web application architecture, including ALB (Application Load Balancer), Web UI, Web Backend, and RDS (Relational Database Service) components.

## Requirements

| Name | Version |
| :-- | :-- |
| terraform | >= 0.13.1 |
| aws | >= 3.29 |

## Resources

| Name | Type |
| :-- | :-- |
| aws_security_group.alb | resource |
| aws_security_group.web_backend | resource |
| aws_security_group.web_ui | resource |
| aws_security_group.rds | resource |
| aws_vpc_security_group_ingress_rule.alb_ports | resource |
| aws_vpc_security_group_egress_rule.alb_egress | resource |
| aws_vpc_security_group_ingress_rule.web_backend_from_web_ui | resource |
| aws_vpc_security_group_egress_rule.web_backend_egress | resource |
| aws_vpc_security_group_ingress_rule.web_ui_from_alb | resource |
| aws_vpc_security_group_egress_rule.web_ui_egress | resource |
| aws_vpc_security_group_ingress_rule.rds_from_web_backend | resource |
| aws_vpc_security_group_egress_rule.rds_block_all_outbound | resource |

## Inputs

| Name | Description | Type | Default | Required |
| :-- | :-- | :-- | :-- | :-- |
| vpc_id | ID of the VPC where security groups will be created | string | n/a | yes |
| name_prefix | Prefix to be used in the name of the security groups | string | n/a | yes |
| web_backend_port | Port for the web backend service | number | 8080 | yes |
| web_ui_port | Port for the web UI service | number | 3000 | yes |
| rds_port | Port for the RDS service | number | 5432 | yes |
| alb_ingress_ports | List of ports to allow ingress traffic to ALB | list(number) | [80] | yes |
| alb_ingress_cidr | CIDR blocks to allow ingress traffic to ALB | list(string) | ["0.0.0.0/0"] | yes |
| tags | A map of tags to add to all resources | map(string) | {} | yes |

## Outputs

| Name | Description |
| :-- | :-- |
| alb_security_group_id | ID of the ALB security group |
| web_backend_security_group_id | ID of the Web Backend security group |
| web_ui_security_group_id | ID of the Web UI security group |
| rds_instance_security_group_id | ID of the RDS instance security group |
| rds_security_group_id | ID of the RDS security group |
| security_groups | Map of all security groups |

### Security Group Flow
This module creates the following security groups with specific rules:

ALB Security Group:

Allows inbound traffic on specified ports (default: 80) from specified CIDR blocks (default: 0.0.0.0/0)
Allows all outbound traffic
Web UI Security Group:

Allows inbound traffic on web_ui_port (default: 3000) from ALB security group
Allows all outbound traffic
Web Backend Security Group:

Allows inbound traffic on web_backend_port (default: 8080) from Web UI security group
Allows all outbound traffic
RDS Security Group:

Allows inbound traffic on rds_port from Web Backend security group
Blocks all outbound traffic