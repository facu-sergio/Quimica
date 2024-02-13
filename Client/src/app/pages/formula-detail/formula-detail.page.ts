import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormulasService } from 'src/app/services/formulasService.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-formula-detail',
  templateUrl: './formula-detail.page.html',
  styleUrls: ['./formula-detail.page.scss'],
})
export class FormulaDetailPage implements OnInit {
  formulaId:string = "";
  formula = null as any;
  formulaCalculada = null as any;
  cantidad:number =0;
  constructor(private formulasService: FormulasService,
     private activateRoute: ActivatedRoute,
     private changeDetectorRef: ChangeDetectorRef) {}

  ngOnInit() {
    
  }

  ionViewWillEnter(){
    this.formulaId = this.activateRoute.snapshot.paramMap.get('id') as string;
    this.formulasService.getFormulBase(this.formulaId).subscribe({
      next:(res:any)=>{
        this.formula = res;

        this.changeDetectorRef.detectChanges();
      },error:(error:any)=>{

      }
    })
  }

  calcular(){

    this.formulasService.calcular(this.formulaId,this.cantidad).subscribe({
      next:(res:any)=>{
        this.formulaCalculada = res;
        console.log(this.formulaCalculada)
        this.changeDetectorRef.detectChanges();
      },error:(error:any)=>{

      }
    })
  }
}
