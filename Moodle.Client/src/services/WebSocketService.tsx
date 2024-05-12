class WebSocketService {
  private static instance: WebSocketService;
  private ws: WebSocket;
  private eventListeners: ((data: any) => void)[] = [];

  private constructor() {
    this.ws = new WebSocket("ws://localhost:5191/api/Event/ws");
    this.ws.onopen = () => {
      console.log("WebSocket kapcsolat megnyitva");
    };

    this.ws.onmessage = (event) => {
      const eventData = event.data;
      // Kezeld az érkező eseményeket, például itt továbbíthatod őket az alkalmazás többi részéhez
      console.log("Kapott üzenet:", eventData);
      alert(eventData);
    };

    this.ws.onclose = () => {
      console.log("WebSocket kapcsolat bezárva");
    };

    this.ws.onerror = (error) => {
      console.error("WebSocket hiba:", error);
    };
  }

  public static getInstance(): WebSocketService {
    if (!WebSocketService.instance) {
      WebSocketService.instance = new WebSocketService();
    }
    return WebSocketService.instance;
  }

  // Küldj egy új eseményt a szervernek
  public sendMessage(message: string) {
    this.ws.send(message);
  }

  // Regisztrál egy eseménykezelőt
  public addListener(listener: (data: any) => void) {
    this.eventListeners.push(listener);
  }

  // Távolítson el egy eseménykezelőt
  public removeListener(listener: (data: any) => void) {
    const index = this.eventListeners.indexOf(listener);
    if (index !== -1) {
      this.eventListeners.splice(index, 1);
    }
  }
}

export default WebSocketService;
