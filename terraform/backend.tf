terraform {
  backend "s3" {
    bucket = ""
    key    = ""
    region = ""
    # use_lockfile = var.lockfile
    encrypt = true
  }
}