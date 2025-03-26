import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from 'src/app/models/car';
import { Company } from 'src/app/models/company';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent {

  carId: number = 0;
  car!: Car; 
  company: Company = Company;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.carId = Number(this.activatedRoute.snapshot.params['carId']);
    this.processImages();
  }

  viewMoreImages(): void {
  
  }

  async processImages(): Promise<void> {
    const filePaths = ['./assets/b1.jpg', './assets/b2.jpg', './assets/b3.jpg'];
  
    for (const filePath of filePaths) {
      const response = await fetch(filePath);
      const blob = await response.blob();
  
      const base64 = await this.blobToBase64(blob);
  
     
      console.log(`INSERT INTO CarImages (CarId, ImageData) VALUES (5, '${base64}');`);
    }
  }

  private blobToBase64(blob: Blob): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onloadend = () => resolve(reader.result as string);
      reader.onerror = reject;
      reader.readAsDataURL(blob);
    });
  }

}
