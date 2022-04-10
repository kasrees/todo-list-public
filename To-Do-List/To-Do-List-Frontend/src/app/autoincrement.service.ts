export class AutoIncrement {
    static i: number = 0

    static getNext(): number {
        return ++this.i; 
    }
}