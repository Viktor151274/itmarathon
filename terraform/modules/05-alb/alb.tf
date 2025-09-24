resource "aws_lb" "this" {
  name                       = var.name
  internal                   = false
  load_balancer_type         = "application"
  security_groups            = [var.security_group_id]
  subnets                    = var.subnet_ids
  enable_deletion_protection = false

  tags = merge(
    var.tags,
    { "Name" = var.name }
  )
}

resource "aws_lb_target_group" "web_ui_react" {
  name        = "${var.name}-ui-react"
  port        = var.web_ui_port
  protocol    = "HTTP"
  vpc_id      = var.vpc_id
  target_type = "instance"

  health_check {
    path                = "/index.html"
    protocol            = "HTTP"
    matcher             = "200"
    interval            = 30
    timeout             = 5
    healthy_threshold   = 2
    unhealthy_threshold = 2
  }

  tags = merge(
    var.tags,
    { "Name" = "${var.name}-ui_react" }
  )
}

resource "aws_lb_target_group" "web_ui_angular" {
  name        = "${var.name}-ui-angular"
  port        = var.web_ui_port
  protocol    = "HTTP"
  vpc_id      = var.vpc_id
  target_type = "instance"

  health_check {
    path                = "/index.html"
    protocol            = "HTTP"
    matcher             = "200"
    interval            = 30
    timeout             = 5
    healthy_threshold   = 2
    unhealthy_threshold = 2
  }

  tags = merge(
    var.tags,
    { "Name" = "${var.name}-angular" }
  )
}

resource "aws_lb_listener" "http" {
  load_balancer_arn = aws_lb.this.arn
  port              = 80
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    
    forward {
      target_group {
        arn  = aws_lb_target_group.web_ui_react.arn
        weight = 50 # 50% to React
      }
      target_group {
        arn  = aws_lb_target_group.web_ui_angular.arn
        weight = 50 # 50% to Angular
      }
    }
  }
}

