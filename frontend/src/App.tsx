import Flem from "./components/flem";
import { QueryProvider } from "./lib/queryProvider";

const App = () =>   
   (
    <>
      <QueryProvider>
        <Flem/>
      </QueryProvider>
    </>
  )

export default App
