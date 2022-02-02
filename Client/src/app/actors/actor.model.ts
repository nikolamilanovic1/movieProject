export interface actorCreationDTO{
  id:number
  name: string;
  dateOfBirth: Date;
  picture: any;
  biography: any;
}

export interface actorDTO{
    id: number;
    name: string;
    dateOfBirth: Date;
    picture: any;
    biography: string;
  }

  export interface actorsMovieDTO{
    id: number;
    name: string;
    character: string;
    picture: string;
  }
