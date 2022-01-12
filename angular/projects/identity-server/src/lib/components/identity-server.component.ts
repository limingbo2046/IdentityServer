import { Component, OnInit } from '@angular/core';
import { IdentityServerService } from '../services/identity-server.service';

@Component({
  selector: 'lib-identity-server',
  template: ` <p>identity-server works!</p> `,
  styles: [],
})
export class IdentityServerComponent implements OnInit {
  constructor(private service: IdentityServerService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
