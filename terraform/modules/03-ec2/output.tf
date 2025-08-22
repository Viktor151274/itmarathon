output "id" {
  description = "The ID of the instance"
  value       = local.instance_id
}

output "public_ip" {
  description = "The public IP address assigned to the instance, if applicable."
  value = try(
    aws_instance.this.public_ip,
    null,
  )
}

output "availability_zone" {
  description = "The availability zone of the created instance"
  value       = data.aws_availability_zones.available.names[0]
}
